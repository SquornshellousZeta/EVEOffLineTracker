Imports eZet.EveLib.Modules
Imports eZet.EveLib.Modules.EveOnlineApi
Imports System.Threading.Tasks

Public Class frmMain

    Private iKeyID As Integer
    Private svCode As String
    Private lCharacterID As Long
    Private sCharacterName As String

    Private iCorpKeyID As Integer
    Private sCorpvCode As String
    Private lCorpCharacterID As Long

    Private character As Character
    Private corporation As Corporation

    Private charSheetResponse As Models.EveApiResponse(Of Models.Character.CharacterSheet)
    Private objSkillsInQueue As Models.Character.SkillQueue
    Private objSkillInTraining As Models.EveApiResponse(Of Models.Character.SkillTraining)

    Private objEve As eZet.EveLib.Modules.Eve
    Private objSkillTree As Models.EveApiResponse(Of Models.Misc.SkillTree)
    Private objTypeNames As Models.EveApiResponse(Of Models.Misc.TypeName)
    Private objRefTypes As Models.EveApiResponse(Of Models.Misc.ReferenceTypes)

    Private dicAllAssets As New Dictionary(Of Long, Asset)
    Private dicSkills As New Dictionary(Of Integer, Models.Misc.SkillTree.Skill)
    Private dicGroups As New Dictionary(Of Integer, EVEGroup)
    Private dicNames As New Dictionary(Of Long, EVEName)
    Private dicCategories As New Dictionary(Of Integer, EVECategory)
    Private dicFlags As New Dictionary(Of Integer, EVEFlag)


    Private listAssets As Models.EveApiResponse(Of Models.Character.AssetList)
    Private listCorpAssets As Models.EveApiResponse(Of Models.Character.AssetList)
    Private listOrders As Models.EveApiResponse(Of Models.Character.MarketOrders)
    Private listCorpOrders As Models.EveApiResponse(Of Models.Character.MarketOrders)
    Private listJournal As Models.EveApiResponse(Of Models.Character.WalletJournal)
    Private listTransactions As Models.EveApiResponse(Of Models.Character.WalletTransactions)
    Private listCharIndustryJobs As Models.EveApiResponse(Of Models.Character.IndustryJobs)
    Private listCorpIndustryJobs As Models.EveApiResponse(Of Models.Character.IndustryJobs)
    'Private listNotifications As List(Of Character.Notification)
    Private listCharBlueprints As Models.EveApiResponse(Of Models.Character.BlueprintList)
    Private listCorpBlueprints As Models.EveApiResponse(Of Models.Character.BlueprintList)

    Private listCorpFacilities As Models.EveApiResponse(Of Models.Corporation.Facilities)

    Private dsAssets As DataSet
    Private dtAssets As DataTable
    Private bsAssets As BindingSource

    Private dsOrdersBuy, dsOrdersSell As DataSet
    Private dtOrdersBuy, dtOrdersSell As DataTable
    Private bsOrdersSell, bsOrdersBuy As BindingSource

    Private dsJournal As DataSet
    Private dtJournal As DataTable
    Private bsJournal As BindingSource

    Private dsTransactions As DataSet
    Private dtTransactions As DataTable
    Private bsTransactions As BindingSource

    Private dsBlueprints As DataSet
    Private dtBlueprints As DataTable
    Private bsBlueprints As BindingSource

    Private dsIndustry As DataSet
    Private dtIndustry As DataTable
    Private bsIndustry As BindingSource

    Private sListOfErrors As String

    Private Const sAPIFilename As String = "APIInformation"
    Private Const sAPIFileExt As String = ".xml"

    Private Const sItemFilename As String = "invTypes.txt"
    Private Const sGroupFilename As String = "invGroups.txt"
    Private Const sNameFilename As String = "invNames.txt"
    Private Const sCategoryFilename As String = "invCategories.txt"
    Private Const sFlagFilename As String = "invFlags.txt"

    Private Enum OrderState As Integer
        'Valid states: 0 = open/active, 1 = closed, 2 = expired (or fulfilled), 3 = cancelled, 4 = pending, 5 = character deleted
        Open = 0
        Closed = 1
        Expired = 2
        Cancelled = 3
        Pending = 4
        CharDeleted = 5
    End Enum

    Private Enum ActivityID As Integer
        Manufacturing = 1
        Researching_Technology = 2
        Researching_Time_Productivity = 3
        Researching_Material_Productivity = 4
        Copying = 5
        Duplicating = 6
        Reverse_Engineering = 7
        Invention = 8
    End Enum

    Private Enum JobStatus As Integer
        Active = 1
        Paused = 2
        Ready = 3
        Cancelled = 102
        Reverted = 103
        Delivered = 104
        Failed = 105
    End Enum

    Private Async Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Await fLoadEVEData()

        Timer1.Enabled = True

    End Sub

    Public Sub InvokeControl(Of T As Control)(ByVal Control As T, ByVal Action As Action(Of T))
        If Control.InvokeRequired Then
            Control.Invoke(New Action(Of T, Action(Of T))(AddressOf InvokeControl), New Object() {Control, Action})
        Else
            Action(Control)
        End If
    End Sub


    Private Async Function fLoadEVEData() As Threading.Tasks.Task

        Dim frm As frmLoading

        sListOfErrors = ""

        TabControl1.Enabled = False

        If IO.File.Exists(sAPIFilename & sAPIFileExt) Then
            fGetAPIInfoFromFile()
        Else
            fDoAPIMenuClick()
        End If

        frm = New frmLoading

        frm.Show()
        UpdateStatus(frm, "Loading EVE Data...")

        frm.BringToFront()

        objEve = New eZet.EveLib.Modules.Eve()
        objSkillTree = Await objEve.GetSkillTreeAsync()
        objRefTypes = Await objEve.GetReferenceTypesAsync()

        frm.BringToFront()
        Await MakeMapsAsync()

        'MakeSkillMap()
        'MakeItemMap()
        'MakeGroupMap()
        'MakeNameMap()
        'MakeCategoryMap()
        'MakeFlagMap()

        UpdateStatus(frm, "Getting API information")
        character = New Character(iKeyID, svCode, lCharacterID)

        If iCorpKeyID > 0 And sCorpvCode <> "" And lCorpCharacterID > 0 Then
            corporation = New Corporation(iCorpKeyID, sCorpvCode, lCorpCharacterID)
            UpdateStatus(frm, "Getting Corp API information")
        End If


        Try


            UpdateStatus(frm, "Loading Character Sheet...")
            charSheetResponse = Await character.GetCharacterSheetAsync()

            Me.tsslCharSheet.Text = "Cached Until: " & charSheetResponse.CachedUntilAsString

 '           Await fLoadCharacterSheet(charSheetResponse.Result)
            Await Task.Run(Sub() fLoadCharacterSheet(charSheetResponse.Result))

        Catch ex As Exception
            sListOfErrors += "Failed to get Character Sheet data" + vbLf
        End Try


        Try

            UpdateStatus(frm, "Loading Character Assets...")
            listAssets = Await character.GetAssetListAsync()
            Me.tsslAssets.Text = "Cached Until: " & listAssets.CachedUntilAsString

        Catch ex As Exception
            sListOfErrors += "Failed to get Character Asset data" + vbLf
        End Try

        If Not IsNothing(corporation) Then

            Try

                UpdateStatus(frm, "Loading Corporation Assets...")
                listCorpAssets = Await corporation.GetAssetListAsync()
                Me.tsslAssets.Text = "Cached Until: " & listCorpAssets.CachedUntilAsString

            Catch ex As Exception
                sListOfErrors += "Failed to get Corporation Asset data" + vbLf
            End Try
        End If

        Await fLoadAssets()

        Try

            UpdateStatus(frm, "Loading Market Orders...")
            listOrders = Await character.GetMarketOrdersAsync()
            Me.tsslOrders.Text = "Cached Until: " & listOrders.CachedUntilAsString

            If corporation IsNot Nothing Then
                listCorpOrders = Await corporation.GetMarketOrdersAsync()
            End If

            Await fLoadMarketOrders()
            cbActiveOrdersOnly.Checked = False
            cbActiveOrdersOnly.Checked = True
        Catch ex As Exception
            sListOfErrors += "Failed to get Market Order data" + vbLf
        End Try

        Try
            UpdateStatus(frm, "Loading Wallet Journal...")
            listJournal = Await character.GetWalletJournalAsync(2560)
            Me.tsslJournal.Text = "Cached Until: " & listJournal.CachedUntilAsString

            Await fLoadJournal()
        Catch ex As Exception
            sListOfErrors += "Failed to get Wallet Journal data" + vbLf
        End Try

        Try
            UpdateStatus(frm, "Loading Wallet Transactions...")
            listTransactions = Await character.GetWalletTransactionsAsync(2560)
            Me.tsslTransactions.Text = "Cached Until: " & listTransactions.CachedUntilAsString

            Await fLoadTransactions()
        Catch ex As Exception
            sListOfErrors += "Failed to get Wallet Transaction data" + vbLf
        End Try

        Try

            UpdateStatus(frm, "Loading Industry Jobs...")
            listCharIndustryJobs = Await character.GetIndustryJobsHistoryAsync()
            If corporation IsNot Nothing Then
                listCorpIndustryJobs = Await corporation.GetIndustryJobsHistoryAsync()
            End If
            Me.tsslIndustry.Text = "Cached Until: " & listCharIndustryJobs.CachedUntilAsString

            Await fLoadIndustry()
        Catch ex As Exception
            sListOfErrors += "Failed to get Industry Job data" + vbLf
        End Try

        Try

            UpdateStatus(frm, "Loading Blueprints...")
            listCharBlueprints = Await character.GetBlueprintsAsync()

            If corporation IsNot Nothing Then
                listCorpBlueprints = Await corporation.GetBlueprintsAsync()
            End If

            Me.tsslNotifications.Text = "Cached Until: " & listCharBlueprints.CachedUntilAsString

            Await fLoadBlueprints()

        Catch ex As Exception
            sListOfErrors += "Failed to get Blueprint data" + vbLf
        End Try

        frm.Close()

        TabControl1.Enabled = True

        'If sListOfErrors.Trim.Length > 0 Then
        '    MessageBox.Show(frmLoading, sListOfErrors, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End If

    End Function

    Private Async Sub fLoadCharacterSheet(charSheet As Models.Character.CharacterSheet)

        Dim iTotalSkillPoints As Integer
        Dim iIntEnhanced, iMemEnhanced, iPerEnhanced, iWilEnhanced, iChaEnhanced As Integer
        Dim objImage As Image
        Dim sImageFile As String

        Try

            iTotalSkillPoints = GetTotalSkillPoints(charSheet.Skills)

            InvokeControl(lblName, Sub(x) x.Text = charSheet.Name)  'Me.lblName.Text = charSheet.Name
            sCharacterName = charSheet.Name
            InvokeControl(lblRace, Sub(x) x.Text = charSheet.Race)  'Me.lblRace.Text = charSheet.Race
            InvokeControl(lblCorporation, Sub(x) x.Text = charSheet.CorporationName)  'Me.lblCorporation.Text = charSheet.CorporationName
            InvokeControl(lblGender, Sub(x) x.Text = charSheet.Gender)  'Me.lblGender.Text = charSheet.Gender
            InvokeControl(lblSkillpoints, Sub(x) x.Text = String.Format("{0:#,0}", iTotalSkillPoints))  'Me.lblSkillpoints.Text = String.Format("{0:#,0}", iTotalSkillPoints)
            InvokeControl(lblCloneInfo, Sub(x) x.Text = String.Format("{0:#,0}", charSheet.CloneSkillPoints) & "  (" & charSheet.CloneName & ")")  'Me.lblCloneInfo.Text = String.Format("{0:#,0}", charSheet.CloneSkillPoints) & "  (" & charSheet.CloneName & ")"
            If iTotalSkillPoints > charSheet.CloneSkillPoints Then
                InvokeControl(lblSkillpoints, Sub(x) x.ForeColor = Color.Red)  'Me.lblSkillpoints.ForeColor = Color.Red
            End If
            InvokeControl(lblBalance, Sub(x) x.Text = String.Format("{0:#,#.00 ISK}", charSheet.Balance))  'Me.lblBalance.Text = String.Format("{0:#,#.00 ISK}", charSheet.Balance)

            If (charSheet.AttributeEnhancers.Intelligence IsNot Nothing) Then
                iIntEnhanced = charSheet.AttributeEnhancers.Intelligence.Value
                InvokeControl(lblIntImplant, Sub(x) x.Text = "+" & CStr(iIntEnhanced))  'Me.lblIntImplant.Text = "+" & CStr(iIntEnhanced)
            Else
                InvokeControl(lblIntImplant, Sub(x) x.Text = "-")  'Me.lblIntImplant.Text = "-"
            End If
            If (charSheet.AttributeEnhancers.Memory IsNot Nothing) Then
                iMemEnhanced = charSheet.AttributeEnhancers.Memory.Value
                InvokeControl(lblMemImplant, Sub(x) x.Text = "+" & CStr(iMemEnhanced))  'Me.lblMemImplant.Text = "+" & CStr(iMemEnhanced)
            Else
                InvokeControl(lblMemImplant, Sub(x) x.Text = "-")  'Me.lblMemImplant.Text = "-"
            End If
            If (charSheet.AttributeEnhancers.Perception IsNot Nothing) Then
                iPerEnhanced = charSheet.AttributeEnhancers.Perception.Value
                InvokeControl(lblPerImplant, Sub(x) x.Text = "+" & CStr(iPerEnhanced))  'Me.lblPerImplant.Text = "+" & CStr(iPerEnhanced)
            Else
                InvokeControl(lblPerImplant, Sub(x) x.Text = "-")  'Me.lblPerImplant.Text = "-"
            End If
            If (charSheet.AttributeEnhancers.Willpower IsNot Nothing) Then
                iWilEnhanced = charSheet.AttributeEnhancers.Willpower.Value
                InvokeControl(lblWillImplant, Sub(x) x.Text = "+" & CStr(iWilEnhanced))  'Me.lblWillImplant.Text = "+" & CStr(iWilEnhanced)
            Else
                InvokeControl(lblWillImplant, Sub(x) x.Text = "-")  'Me.lblWillImplant.Text = "-"
            End If
            If (charSheet.AttributeEnhancers.Charisma IsNot Nothing) Then
                iChaEnhanced = charSheet.AttributeEnhancers.Charisma.Value
                InvokeControl(lblChaImplant, Sub(x) x.Text = "+" & CStr(iChaEnhanced))  'Me.lblChaImplant.Text = "+" & CStr(iChaEnhanced)
            Else
                InvokeControl(lblChaImplant, Sub(x) x.Text = "-")  'Me.lblChaImplant.Text = "-"
            End If

            Dim sTotalInt, sTotalMem, sTotalPer, sTotalWil, sTotalCha As String

            sTotalInt = String.Format("{0:#.00}", charSheet.Attributes.Intelligence + iIntEnhanced) & "  (" & _
                        String.Format("{0:#}", charSheet.Attributes.Intelligence) & ")"
            sTotalMem = String.Format("{0:#.00}", charSheet.Attributes.Memory + iMemEnhanced) & "  (" & _
                            String.Format("{0:#}", charSheet.Attributes.Memory) & ")"
            sTotalPer = String.Format("{0:#.00}", charSheet.Attributes.Perception + iPerEnhanced) & "  (" & _
                            String.Format("{0:#}", charSheet.Attributes.Perception) & ")"
            sTotalWil = String.Format("{0:#.00}", charSheet.Attributes.Willpower + iWilEnhanced) & "  (" & _
                            String.Format("{0:#}", charSheet.Attributes.Willpower) & ")"
            sTotalCha = String.Format("{0:#.00}", charSheet.Attributes.Charisma + iChaEnhanced) & "  (" & _
                            String.Format("{0:#}", charSheet.Attributes.Charisma) & ")"

            InvokeControl(lblInt, Sub(x) x.Text = sTotalInt)
            InvokeControl(lblMem, Sub(x) x.Text = sTotalMem)
            InvokeControl(lblPer, Sub(x) x.Text = sTotalPer)
            InvokeControl(lblWill, Sub(x) x.Text = sTotalWil)
            InvokeControl(lblCha, Sub(x) x.Text = sTotalCha)

            objImage = New Image()

            Await objImage.GetCharacterPortraitAsync(lCharacterID, eZet.EveLib.Modules.Image.CharacterPortraitSize.X256, "D:\\temp")
            sImageFile = IO.Path.Combine("D:\temp", lCharacterID.ToString & "_" & eZet.EveLib.Modules.Image.CharacterPortraitSize.X256 & ".jpg")
            InvokeControl(picImage, Sub(x) x.Image = System.Drawing.Image.FromFile(sImageFile))   'Me.picImage.Image = System.Drawing.Image.FromFile(sImageFile)

            Await objImage.GetCorporationLogoAsync(charSheet.CorporationId, eZet.EveLib.Modules.Image.CorporationLogoSize.X64, "D:\\temp")
            sImageFile = IO.Path.Combine("D:\temp", charSheet.CorporationId.ToString & "_" & eZet.EveLib.Modules.Image.CorporationLogoSize.X64 & ".png")
            InvokeControl(imgCorp, Sub(x) x.Image = System.Drawing.Image.FromFile(sImageFile))   'Me.imgCorp.Image = System.Drawing.Image.FromFile(sImageFile)

            If charSheet.AllianceId > 0 Then
                Await objImage.GetAllianceLogoAsync(charSheet.AllianceId, eZet.EveLib.Modules.Image.AllianceLogoSize.X64, "D:\\temp")
                sImageFile = IO.Path.Combine("D:\temp", lCharacterID.ToString & "_" & eZet.EveLib.Modules.Image.AllianceLogoSize.X64 & ".png")
                InvokeControl(imgAlliance, Sub(x) x.Image = System.Drawing.Image.FromFile(sImageFile))   'Me.imgAlliance.Image = System.Drawing.Image.FromFile(sImageFile)
            End If

            objSkillInTraining = character.GetSkillTraining
            If objSkillInTraining.Result.TypeId = 0 Then
                objSkillsInQueue = Nothing
            Else
                objSkillsInQueue = character.GetSkillQueue.Result
            End If

            fUpdateSkillTimes(objSkillsInQueue)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Private Function GetTotalSkillPoints(argSkills As Models.EveOnlineRowCollection(Of Models.Character.Skill)) As Integer

        Dim iTotal As Integer = 0

        For Each objSkill As Models.Character.Skill In argSkills
            iTotal += objSkill.Skillpoints
        Next

        Return iTotal

    End Function

    Private Sub fUpdateSkillTimes(argSkillsInQueue As Models.Character.SkillQueue)

        Dim objSkill As Models.Character.SkillQueue.Skill
        Dim spanRemaining As TimeSpan
        Dim sRemaining As String

        InvokeControl(lbSkillQueue, Sub(x) lbSkillQueue.Items.Clear())    'lbSkillQueue.Items.Clear()
        InvokeControl(lbSkillLevel, Sub(x) lbSkillLevel.Items.Clear())    'lbSkillLevel.Items.Clear()
        InvokeControl(lbSkillTimeRemaining, Sub(x) lbSkillTimeRemaining.Items.Clear())    'lbSkillTimeRemaining.Items.Clear()

        If argSkillsInQueue IsNot Nothing Then
            For Each objSkill In argSkillsInQueue.Queue
                InvokeControl(lbSkillQueue, Sub(x) lbSkillQueue.Items.Add(dicSkills(objSkill.TypeId).TypeName))    'lbSkillQueue.Items.Add(dicSkills(objSkill.TypeId).TypeName)
                InvokeControl(lbSkillLevel, Sub(x) lbSkillLevel.Items.Add(objSkill.Level))    'lbSkillLevel.Items.Add(objSkill.Level)
                If IsNothing(objSkill.EndTime) Then
                    sRemaining = "Paused"
                Else
                    If objSkill.Equals(argSkillsInQueue.Queue(0)) Then
                        spanRemaining = objSkill.EndTime - Now()
                    Else
                        spanRemaining = objSkill.EndTime - objSkill.StartTime
                    End If
                    sRemaining = fGetTimeString(spanRemaining)

                End If
                InvokeControl(lbSkillTimeRemaining, Sub(x) lbSkillTimeRemaining.Items.Add(sRemaining))    'lbSkillTimeRemaining.Items.Add(sRemaining)
            Next
        End If

    End Sub

    Private Function fGetTimeString(ByVal objTime As TimeSpan) As String

        Dim sValue As String = "0s"

        If objTime.TotalSeconds < 0 Then
            Return "-"
        End If

        If objTime.Days > 0 Then
            sValue = String.Format("{0:D1}d {1:D1}h {2:D1}m {3:D1}s", _
                objTime.Days, _
                objTime.Hours, _
                objTime.Minutes, _
                objTime.Seconds)
        ElseIf objTime.Hours > 0 Then
            sValue = String.Format("{0:D1}h {1:D1}m {2:D1}s", _
                objTime.Hours, _
                objTime.Minutes, _
                objTime.Seconds)
        ElseIf objTime.Minutes > 0 Then
            sValue = String.Format("{0:D1}m {1:D1}s", _
                objTime.Minutes, _
                objTime.Seconds)
        ElseIf objTime.Seconds > 0 Then
            sValue = String.Format("{0:D1}s", _
                objTime.Seconds)
        End If

        Return sValue

    End Function

    Private Async Function fLoadAssets() As Task

        Dim objAsset As Models.Character.AssetList.Item

        Dim StringType As System.Type = Type.GetType("System.String")
        Dim LongType As System.Type = Type.GetType("System.Int64")

        Try

            dsAssets = New DataSet
            dtAssets = New DataTable

            dtAssets.Columns.Add("Name", StringType)
            dtAssets.Columns.Add("Group", StringType)
            dtAssets.Columns.Add("Category", StringType)
            dtAssets.Columns.Add("Container", StringType)
            dtAssets.Columns.Add("Qty", LongType)
            dtAssets.Columns.Add("Location", StringType)
            dtAssets.Columns.Add("CharCorp", StringType)
            dtAssets.TableName = "Assets"

            For Each objAsset In listAssets.Result.Items
                fAddAssetToTable(objAsset, True, Nothing, 0, Nothing)
            Next

            If Not IsNothing(listCorpAssets) Then
                For Each objAsset In listCorpAssets.Result.Items
                    fAddAssetToTable(objAsset, False, Nothing, 0, Nothing)
                Next
            End If

            dsAssets.Tables.Add(dtAssets)

            bsAssets = New BindingSource
            bsAssets.DataSource = dsAssets
            bsAssets.DataMember = dsAssets.Tables(0).TableName

            dgvAssets.DataSource = bsAssets
            dgvAssets.Visible = True

            dgvAssets.Columns("Qty").HeaderText = "Quantity"
            dgvAssets.Columns("CharCorp").HeaderText = "Char/Corp"

            dgvAssets.Sort(dgvAssets.Columns("Name"), System.ComponentModel.ListSortDirection.Ascending)

            dgvAssets.Columns("Name").FillWeight = 100
            dgvAssets.Columns("Group").FillWeight = 75
            dgvAssets.Columns("Category").FillWeight = 50
            dgvAssets.Columns("Container").FillWeight = 75
            dgvAssets.Columns("Qty").FillWeight = 35
            dgvAssets.Columns("Location").FillWeight = 75
            dgvAssets.Columns("CharCorp").FillWeight = 25

            dgvAssets.Columns("Qty").DefaultCellStyle.Format = "#,##0"

            dgvAssets.Columns("Qty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Private Sub fAddAssetToTable(ByVal objAsset As Models.Character.AssetList.Item, ByVal bCharAsset As Boolean, ByVal objParent As Models.Character.AssetList.Item, ByVal iLocationID As Long, sContainer As String)

        Dim drAssets As DataRow
        Dim objAssetInside As Models.Character.AssetList.Item
        Dim objItem As EVEItem
        Dim objNewAsset As Asset
        Dim sFlag As String

        Try

            objItem = EVEItem.Find(objAsset.TypeId)

            If objAsset.Items IsNot Nothing Then
                For Each objAssetInside In objAsset.Items
                    If objAsset.LocationId > 0 Then
                        If objParent IsNot Nothing Then
                            sFlag = IIf(objParent.Flag > 0 And Not dicFlags(objAsset.Flag).FlagName.Equals("Locked") And Not dicFlags(objAsset.Flag).FlagName.Equals("Unlocked"), " ( " & dicFlags(objParent.Flag).FlagText & " )", "").ToString
                            fAddAssetToTable(objAssetInside, bCharAsset, objAsset, objAsset.LocationId, EVEItem.Find(objParent.TypeId).TypeName & sFlag & " / " & objItem.TypeName)
                        Else
                            fAddAssetToTable(objAssetInside, bCharAsset, objAsset, objAsset.LocationId, objItem.TypeName)
                        End If
                    Else
                        If objParent IsNot Nothing Then
                            sFlag = IIf(objParent.Flag > 0 And Not dicFlags(objAsset.Flag).FlagName.Equals("Locked") And Not dicFlags(objAsset.Flag).FlagName.Equals("Unlocked"), " ( " & dicFlags(objParent.Flag).FlagText & " )", "").ToString
                            If objParent.LocationId > 0 Then
                                fAddAssetToTable(objAssetInside, bCharAsset, objAsset, objParent.LocationId, EVEItem.Find(objParent.TypeId).TypeName & sFlag & " / " & objItem.TypeName)
                            Else
                                fAddAssetToTable(objAssetInside, bCharAsset, objAsset, 0, EVEItem.Find(objParent.TypeId).TypeName & sFlag & " / " & objItem.TypeName)
                            End If
                        Else
                            fAddAssetToTable(objAssetInside, bCharAsset, objAsset, objAsset.LocationId, objItem.TypeName)
                        End If
                    End If
                Next
            End If

            objNewAsset = New Asset
            objNewAsset.ItemID = objAsset.ItemId
            objNewAsset.TypeID = objAsset.TypeId

            drAssets = dtAssets.NewRow
            If objAsset.TypeId = 0 Then
                drAssets("Name") = "Unknown"
                drAssets("Group") = "Unknown"
                drAssets("Category") = "Unknown"
            Else
                drAssets("Name") = objItem.TypeName
                drAssets("Group") = dicGroups(objItem.GroupID).GroupName
                drAssets("Category") = dicCategories(dicGroups(objItem.GroupID).CategoryID).CategoryName
            End If

            If iLocationID = 0 Then
                If objAsset.LocationId = 0 Then
                    If objParent.LocationId = 0 Then
                        drAssets("Location") = "Unknown?"
                    Else
                        drAssets("Location") = NameLookup(objParent.LocationId)
                    End If
                Else
                    drAssets("Location") = NameLookup(objAsset.LocationId)
                End If
            Else
                drAssets("Location") = NameLookup(iLocationID)
            End If

            objNewAsset.Location = drAssets("Location").ToString

            'If objParent IsNot Nothing Then
            '    If objParent.Items IsNot Nothing Then
            '        objParentItem = dicItems(objParent.TypeId)
            '        If dicFlags(objAsset.Flag).FlagText = "None" Then
            '            drAssets("Container") = objParentItem.TypeName
            '        Else
            '            drAssets("Container") = objParentItem.TypeName & " - " & dicFlags(objAsset.Flag).FlagText
            '        End If
            '    Else
            '        drAssets("Container") = "Unknown - " & dicFlags(objParent.Flag).FlagText
            '    End If
            'Else
            '    drAssets("Container") = "Hanger"
            'End If
            If sContainer IsNot Nothing Then
                If dicFlags(objAsset.Flag).FlagText = "None" Then
                    drAssets("Container") = sContainer
                Else
                    If Not dicFlags(objAsset.Flag).FlagName.Equals("Locked") And Not dicFlags(objAsset.Flag).FlagName.Equals("Unlocked") Then
                        drAssets("Container") = sContainer & " ( " & dicFlags(objAsset.Flag).FlagText & " )"
                    Else
                        drAssets("Container") = sContainer
                    End If
                End If
                'drAssets("Container") = sContainer
            Else
                If bCharAsset Then
                    drAssets("Container") = "Hanger"
                Else
                    drAssets("Container") = "Space"
                End If
            End If

            objNewAsset.Container = drAssets("Container").ToString

            'If objLocation IsNot Nothing Then
            '    drAssets("Location") = objLocation.Name
            'ElseIf objAsset.LocationStation IsNot Nothing Then
            '    drAssets("Location") = objAsset.LocationStation.Name
            'ElseIf objParent IsNot Nothing Then
            '    If objParent.LocationStation IsNot Nothing Then
            '        drAssets("Location") = objParent.LocationStation.Name
            '    ElseIf objParent.Container <> EveAI.Product.ContainerType.None Then
            '        drAssets("Location") = objParent.Container.ToString
            '    ElseIf objParent.LocationSolarsystem IsNot Nothing Then
            '        drAssets("Location") = objParent.LocationSolarsystem
            '    Else
            '        drAssets("Location") = "Unknown"
            '    End If
            'Else
            '    drAssets("Location") = "Unknown"
            'End If
            drAssets("Qty") = objAsset.Quantity
            drAssets("CharCorp") = CStr(IIf(bCharAsset, "Char", "Corp"))

            objNewAsset.Quantity = objAsset.Quantity
            objNewAsset.IsCharacterAsset = bCharAsset

            If Not dicAllAssets.ContainsKey(objNewAsset.ItemID) Then
                dicAllAssets.Add(objNewAsset.ItemID, objNewAsset)
            End If

            dtAssets.Rows.Add(drAssets)
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try


    End Sub

    Private Async Function fLoadMarketOrders() As Task

        Dim objOrder As Models.Character.MarketOrders.MarketOrder
        Dim drOrder As DataRow
        Dim iOpenOrders As Integer
        Dim dEscrow, dSellOrdersTotal, dBuyOrdersTotal As Double

        Dim StringType As System.Type = Type.GetType("System.String")
        Dim LongType As System.Type = Type.GetType("System.Int64")
        Dim IntegerType As System.Type = Type.GetType("System.Int32")
        Dim DateType As System.Type = Type.GetType("System.DateTime")
        Dim DoubleType As System.Type = Type.GetType("System.Double")

        Try

            dsOrdersBuy = New DataSet
            dsOrdersSell = New DataSet
            dtOrdersBuy = New DataTable
            dtOrdersSell = New DataTable

            dtOrdersBuy.Columns.Add("ItemName", StringType)
            dtOrdersBuy.Columns.Add("State", StringType)
            dtOrdersBuy.Columns.Add("Volume", StringType)
            dtOrdersBuy.Columns.Add("Price", DoubleType)
            dtOrdersBuy.Columns.Add("Station", StringType)
            dtOrdersBuy.Columns.Add("Range", StringType)
            dtOrdersBuy.Columns.Add("MinSellVolume", StringType)
            dtOrdersBuy.Columns.Add("Expires", DateType)
            dtOrdersBuy.Columns.Add("Issued", DateType)
            dtOrdersBuy.Columns.Add("System", StringType)

            dtOrdersSell.Columns.Add("ItemName", StringType)
            dtOrdersSell.Columns.Add("State", StringType)
            dtOrdersSell.Columns.Add("Volume", StringType)
            dtOrdersSell.Columns.Add("Price", DoubleType)
            dtOrdersSell.Columns.Add("Station", StringType)
            dtOrdersSell.Columns.Add("Expires", DateType)
            dtOrdersSell.Columns.Add("Issued", DateType)
            dtOrdersSell.Columns.Add("System", StringType)

            dtOrdersBuy.TableName = "OrdersBuy"
            dtOrdersSell.TableName = "OrdersSell"

            For Each objOrder In listOrders.Result.Orders

                If objOrder.Bid = 1 Then
                    drOrder = dtOrdersBuy.NewRow
                    If objOrder.Range = 0 Then
                        drOrder("Range") = "System"
                    ElseIf objOrder.Range = 32767 Then
                        drOrder("Range") = "Region"
                    ElseIf objOrder.Range = -1 Then
                        drOrder("Range") = "Station"
                    Else
                        drOrder("Range") = objOrder.Range.ToString & " Jumps"
                    End If
                    drOrder("MinSellVolume") = objOrder.MinVolume.ToString
                Else
                    drOrder = dtOrdersSell.NewRow
                End If

                If objOrder.TypeId = 0 Then
                    drOrder("ItemName") = "Unknown"
                Else
                    drOrder("ItemName") = EVEItem.Find(objOrder.TypeId).TypeName
                End If

                drOrder("Expires") = objOrder.IssuedDate.ToLocalTime.AddDays(CDbl(objOrder.Duration))

                drOrder("Issued") = objOrder.IssuedDate.ToLocalTime.ToShortDateString & " " & objOrder.IssuedDate.ToLocalTime.ToShortTimeString
                drOrder("State") = GetOrderStateString(CType(objOrder.OrderState, OrderState))
                drOrder("Price") = CDbl(objOrder.Price)
                drOrder("Station") = dicNames(objOrder.StationId).ItemName
                drOrder("System") = Split(dicNames(objOrder.StationId).ItemName, " ")(0)
                drOrder("Volume") = objOrder.VolumeRemaining.ToString & " / " & objOrder.VolumeEntered.ToString

                If objOrder.Bid = 1 Then
                    dtOrdersBuy.Rows.Add(drOrder)
                Else
                    dtOrdersSell.Rows.Add(drOrder)
                End If

                If objOrder.OrderState = OrderState.Open Then
                    iOpenOrders += 1
                End If

                dEscrow += objOrder.Escrow

                If objOrder.Bid = 1 Then
                    dBuyOrdersTotal += objOrder.Price * objOrder.VolumeRemaining
                Else
                    dSellOrdersTotal += objOrder.Price * objOrder.VolumeRemaining
                End If
            Next

            dsOrdersBuy.Tables.Add(dtOrdersBuy)
            dsOrdersSell.Tables.Add(dtOrdersSell)

            bsOrdersSell = New BindingSource
            bsOrdersBuy = New BindingSource

            bsOrdersSell.DataSource = dsOrdersSell
            bsOrdersSell.DataMember = dsOrdersSell.Tables(0).TableName
            bsOrdersBuy.DataSource = dsOrdersBuy
            bsOrdersBuy.DataMember = dsOrdersBuy.Tables(0).TableName

            dgvOrdersBuy.DataSource = bsOrdersBuy
            dgvOrdersSell.DataSource = bsOrdersSell

            dgvOrdersBuy.Columns("ItemName").HeaderText = "Item"
            dgvOrdersBuy.Columns("Volume").HeaderText = "Quantity"
            dgvOrdersBuy.Columns("MinSellVolume").HeaderText = "Min Volume"
            dgvOrdersSell.Columns("ItemName").HeaderText = "Item"
            dgvOrdersSell.Columns("Volume").HeaderText = "Quantity"

            dgvOrdersBuy.Columns("ItemName").FillWeight = 120
            dgvOrdersBuy.Columns("State").FillWeight = 40
            dgvOrdersBuy.Columns("Volume").FillWeight = 35
            dgvOrdersBuy.Columns("Price").FillWeight = 60
            dgvOrdersBuy.Columns("MinSellVolume").FillWeight = 50
            dgvOrdersBuy.Columns("Range").FillWeight = 35
            dgvOrdersBuy.Columns("Expires").FillWeight = 90
            dgvOrdersBuy.Columns("Station").FillWeight = 100
            dgvOrdersBuy.Columns("Issued").FillWeight = 75
            dgvOrdersBuy.Columns("System").FillWeight = 40

            dgvOrdersSell.Columns("ItemName").FillWeight = 120
            dgvOrdersSell.Columns("State").FillWeight = 40
            dgvOrdersSell.Columns("Volume").FillWeight = 35
            dgvOrdersSell.Columns("Price").FillWeight = 60
            dgvOrdersSell.Columns("Station").FillWeight = 100
            dgvOrdersSell.Columns("Expires").FillWeight = 75
            dgvOrdersSell.Columns("Issued").FillWeight = 75
            dgvOrdersSell.Columns("System").FillWeight = 40

            dgvOrdersBuy.Visible = True
            dgvOrdersSell.Visible = True

            dgvOrdersBuy.Columns("Price").DefaultCellStyle.Format = "#,#.00 ISK"
            dgvOrdersSell.Columns("Price").DefaultCellStyle.Format = "#,#.00 ISK"

            dgvOrdersBuy.Sort(dgvOrdersBuy.Columns("Issued"), System.ComponentModel.ListSortDirection.Descending)
            dgvOrdersSell.Sort(dgvOrdersSell.Columns("Issued"), System.ComponentModel.ListSortDirection.Descending)

            dgvOrdersBuy.Columns("Volume").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvOrdersSell.Columns("Volume").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            dgvOrdersBuy.Columns("Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvOrdersSell.Columns("Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            lblNumOrders.Text = "Total of " & iOpenOrders & " orders outstanding"
            lblNumOrders.Text += ", Total in Escrow: " & dEscrow.ToString("N2") & " ISK"

            lblSellOrdersTitle.Text = "Sell Orders Total: " & dSellOrdersTotal.ToString("N2") & " ISK"
            lblBuyOrdersTitle.Text = "Buy Orders Total: " & dBuyOrdersTotal.ToString("N2") & " ISK"

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Private Function GetOrderStateString(argOrderState As OrderState) As String

        Select Case argOrderState
            Case OrderState.Open
                Return "Open"
            Case OrderState.Closed
                Return "Closed"
            Case OrderState.Expired
                Return "Expired"
            Case OrderState.Cancelled
                Return "Cancelled"
            Case OrderState.Pending
                Return "Pending"
            Case OrderState.CharDeleted
                Return "Character Deleted"
            Case Else
                Return "Unknown"
        End Select

    End Function

    Private Async Function fLoadJournal() As Task

        Dim objEntry As Models.Character.WalletJournal.JournalEntry
        Dim drEntry As DataRow
        Dim dTaxAmount As Double = 0

        Dim StringType As System.Type = Type.GetType("System.String")
        Dim DoubleType As System.Type = Type.GetType("System.Double")
        Dim DateType As System.Type = Type.GetType("System.DateTime")

        Try

            dsJournal = New DataSet
            dtJournal = New DataTable

            dtJournal.Columns.Add("EntryTimeCol", DateType)
            dtJournal.Columns.Add("TypeCol", StringType)
            dtJournal.Columns.Add("AmountCol", DoubleType)
            dtJournal.Columns.Add("BalanceCol", DoubleType)
            dtJournal.Columns.Add("DescriptionCol", StringType)

            dtJournal.TableName = "WalletJournal"

            For Each objEntry In listJournal.Result.Journal

                Double.TryParse(objEntry.TaxAmount, dTaxAmount)

                If (dTaxAmount > 0.0) Then
                    drEntry = dtJournal.NewRow

                    drEntry("EntryTimeCol") = objEntry.Date.ToLocalTime
                    drEntry("TypeCol") = objRefTypes.Result.RefTypes(objEntry.RefTypeId).RefTypeName
                    drEntry("AmountCol") = -dTaxAmount
                    drEntry("BalanceCol") = objEntry.BalanceAfter - dTaxAmount
                    drEntry("DescriptionCol") = String.Format("Corporation tax of {0} paid", _
                        FormatPercent((dTaxAmount / (objEntry.Amount + dTaxAmount)), 1))
                    dtJournal.Rows.Add(drEntry)

                End If

                drEntry = dtJournal.NewRow

                drEntry("EntryTimeCol") = objEntry.Date.ToLocalTime
                drEntry("TypeCol") = objRefTypes.Result.RefTypes(objEntry.RefTypeId).RefTypeName
                If (dTaxAmount > 0.0) Then
                    drEntry("AmountCol") = objEntry.Amount + dTaxAmount
                Else
                    drEntry("AmountCol") = objEntry.Amount
                End If
                drEntry("BalanceCol") = objEntry.BalanceAfter
                Select Case objEntry.RefTypeId
                    Case 2
                        drEntry("DescriptionCol") = String.Format("Market: {0} bought stuff from {1}", _
                                                objEntry.OwnerName, objEntry.ParticipantName)
                    Case 42
                        drEntry("DescriptionCol") = String.Format("{0} bought stuff on the market", _
                                                objEntry.OwnerName)
                    Case 54
                        drEntry("DescriptionCol") = String.Format("Sales tax paid to {0}", _
                                                objEntry.ParticipantName)
                    Case 85
                        drEntry("DescriptionCol") = String.Format("Bounty paid to {0} for killing pirates in {1}", _
                                                objEntry.ParticipantName, objEntry.ArgumentName)
                    Case 96
                        drEntry("DescriptionCol") = String.Format("Planetary Import Tax: {0} imported from {1} (paid to {2})", _
                                                objEntry.OwnerName, objEntry.ArgumentName, objEntry.ParticipantName)
                    Case 97
                        drEntry("DescriptionCol") = String.Format("Planetary Export Tax: {0} exported from {1} (paid to {2})", _
                                                objEntry.OwnerName, objEntry.ArgumentName, objEntry.ParticipantName)
                    Case Else
                        drEntry("DescriptionCol") = objRefTypes.Result.RefTypes(objEntry.RefTypeId).RefTypeName
                End Select

                dtJournal.Rows.Add(drEntry)



            Next

            dsJournal.Tables.Add(dtJournal)

            bsJournal = New BindingSource

            bsJournal.DataSource = dsJournal
            bsJournal.DataMember = dsJournal.Tables(0).TableName

            dgvJournal.DataSource = bsJournal

            dgvJournal.Visible = True

            dgvJournal.Sort(dgvJournal.Columns("EntryTimeCol"), System.ComponentModel.ListSortDirection.Descending)

            dgvJournal.Columns("AmountCol").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvJournal.Columns("BalanceCol").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            dgvJournal.Columns("EntryTimeCol").DefaultCellStyle.Format = "yyyy.MM.dd HH:mm"
            dgvJournal.Columns("AmountCol").DefaultCellStyle.Format = "#,#.00 ISK"
            dgvJournal.Columns("BalanceCol").DefaultCellStyle.Format = "#,#.00 ISK"

            dgvJournal.Columns("EntryTimeCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvJournal.Columns("TypeCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvJournal.Columns("AmountCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvJournal.Columns("BalanceCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvJournal.Columns("DescriptionCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            dgvJournal.Columns("EntryTimeCol").Width = 100
            dgvJournal.Columns("TypeCol").Width = 150
            dgvJournal.Columns("AmountCol").Width = 105
            dgvJournal.Columns("BalanceCol").Width = 125

            dgvJournal.Columns("EntryTimeCol").HeaderText = "Date"
            dgvJournal.Columns("TypeCol").HeaderText = "Type"
            dgvJournal.Columns("AmountCol").HeaderText = "Amount"
            dgvJournal.Columns("BalanceCol").HeaderText = "Balance"
            dgvJournal.Columns("DescriptionCol").HeaderText = "Description"

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Private Async Function fLoadTransactions() As Task

        Dim objEntry As Models.Character.WalletTransactions.Transaction
        Dim drEntry As DataRow

        Dim StringType As System.Type = Type.GetType("System.String")
        Dim DoubleType As System.Type = Type.GetType("System.Double")
        Dim DateType As System.Type = Type.GetType("System.DateTime")
        Dim IntegerType As System.Type = Type.GetType("System.Int64")

        dsTransactions = New DataSet
        dtTransactions = New DataTable

        dtTransactions.Columns.Add("EntryTimeCol", DateType)
        dtTransactions.Columns.Add("TypeCol", StringType)
        dtTransactions.Columns.Add("PriceCol", DoubleType)
        dtTransactions.Columns.Add("QtyCol", IntegerType)
        dtTransactions.Columns.Add("CreditCol", DoubleType)
        dtTransactions.Columns.Add("ClientCol", StringType)
        dtTransactions.Columns.Add("WhereCol", StringType)

        dtTransactions.TableName = "WalletTransactions"

        For Each objEntry In listTransactions.Result.Transactions
            drEntry = dtTransactions.NewRow

            If objEntry.TransactionType = Models.OrderType.Buy Then
                drEntry("CreditCol") = -objEntry.Price * objEntry.Quantity
            Else
                drEntry("CreditCol") = objEntry.Price * objEntry.Quantity
            End If
            drEntry("EntryTimeCol") = objEntry.TransactionDate.ToLocalTime
            drEntry("TypeCol") = objEntry.TypeName
            drEntry("PriceCol") = CDbl(objEntry.Price)
            drEntry("QtyCol") = CDbl(objEntry.Quantity)
            drEntry("ClientCol") = objEntry.ClientName
            drEntry("WhereCol") = objEntry.StationName

            dtTransactions.Rows.Add(drEntry)

        Next

        dsTransactions.Tables.Add(dtTransactions)

        bsTransactions = New BindingSource

        bsTransactions.DataSource = dsTransactions
        bsTransactions.DataMember = dsTransactions.Tables(0).TableName

        dgvTransactions.DataSource = bsTransactions

        dgvTransactions.Visible = True

        dgvTransactions.Sort(dgvTransactions.Columns("EntryTimeCol"), System.ComponentModel.ListSortDirection.Descending)

        dgvTransactions.Columns("PriceCol").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvTransactions.Columns("QtyCol").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvTransactions.Columns("CreditCol").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        dgvTransactions.Columns("EntryTimeCol").DefaultCellStyle.Format = "yyyy.MM.dd HH:mm"
        dgvTransactions.Columns("PriceCol").DefaultCellStyle.Format = "#,#.00 ISK"
        dgvTransactions.Columns("CreditCol").DefaultCellStyle.Format = "#,#.00 ISK"

        dgvTransactions.Columns("EntryTimeCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgvTransactions.Columns("TypeCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgvTransactions.Columns("PriceCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgvTransactions.Columns("QtyCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgvTransactions.Columns("CreditCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgvTransactions.Columns("ClientCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        dgvTransactions.Columns("WhereCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        dgvTransactions.Columns("EntryTimeCol").Width = 100
        dgvTransactions.Columns("TypeCol").Width = 150
        dgvTransactions.Columns("PriceCol").Width = 105
        dgvTransactions.Columns("QtyCol").Width = 50
        dgvTransactions.Columns("CreditCol").Width = 105
        dgvTransactions.Columns("ClientCol").Width = 150

        dgvTransactions.Columns("EntryTimeCol").HeaderText = "When"
        dgvTransactions.Columns("TypeCol").HeaderText = "Type"
        dgvTransactions.Columns("PriceCol").HeaderText = "Price"
        dgvTransactions.Columns("QtyCol").HeaderText = "Quantity"
        dgvTransactions.Columns("CreditCol").HeaderText = "Credit"
        dgvTransactions.Columns("ClientCol").HeaderText = "Client"
        dgvTransactions.Columns("WhereCol").HeaderText = "Where"

    End Function


    Private Async Function fLoadIndustry() As Task

        Dim objEntry As Models.Character.IndustryJobs.NewIndustryJob
        Dim drEntry As DataRow

        Dim StringType As System.Type = Type.GetType("System.String")
        Dim DateType As System.Type = Type.GetType("System.DateTime")
        Dim IntegerType As System.Type = Type.GetType("System.Int64")

        Dim spanRemaining As TimeSpan
        Dim sRemainingTime As String = ""

        Try

            dsIndustry = New DataSet
            dtIndustry = New DataTable

            dtIndustry.Columns.Add("StatusCol", StringType)
            dtIndustry.Columns.Add("TimeRemainingCol", IntegerType)
            dtIndustry.Columns.Add("JobRunsCol", StringType)
            dtIndustry.Columns.Add("ActivityCol", StringType)
            dtIndustry.Columns.Add("BlueprintCol", StringType)
            dtIndustry.Columns.Add("FacilityCol", StringType)
            dtIndustry.Columns.Add("InstallerCol", StringType)
            dtIndustry.Columns.Add("InstallDateCol", DateType)
            dtIndustry.Columns.Add("EndDateCol", DateType)
            dtIndustry.Columns.Add("CharCorpCol", StringType)

            dtIndustry.TableName = "IndustryJobs"

            'Dim lookup As New EveAI.Live.Generic.CharacterNameLookupApi()
            'Dim sCorpName As String = api.GetCharacterSheet.CorporationName

            'If listCharIndustryJobs IsNot Nothing Then
            '    For Each objEntry In listCharIndustryJobs.Result.Jobs
            '        If Not lookup.CharacterIDsToLookup.Contains(objEntry.InstallerId) Then
            '            lookup.CharacterIDsToLookup.Add(objEntry.InstallerId)
            '        End If
            '    Next
            'End If

            'If listCorpIndustryJobs IsNot Nothing Then
            '    For Each objEntry In listCorpIndustryJobs
            '        If Not lookup.CharacterIDsToLookup.Contains(objEntry.InstallerID) Then
            '            lookup.CharacterIDsToLookup.Add(objEntry.InstallerID)
            '        End If
            '    Next
            'End If

            'lookup.UpdateData()

            If Not corporation Is Nothing Then
                listCorpFacilities = Await corporation.GetFacilitiesAsync()
            End If

            Debug.Print(listCorpFacilities.Result.FacilityEntries(0).ToString())


            If listCharIndustryJobs.Result.Jobs IsNot Nothing Then

                For Each objEntry In listCharIndustryJobs.Result.Jobs
                    drEntry = dtIndustry.NewRow

                    If fGetStatus(Integer.Parse(objEntry.Status)).Equals("Ready") Then
                        drEntry("StatusCol") = "Deliver"
                        drEntry("TimeRemainingCol") = 0
                    ElseIf fGetStatus(Integer.Parse(objEntry.Status)).Equals("Active") Then
                        spanRemaining = (objEntry.EndDate.ToLocalTime - Now)
                        If spanRemaining.TotalSeconds < 0 Then
                            drEntry("StatusCol") = "Deliver"
                            drEntry("TimeRemainingCol") = 0
                        Else
                            If spanRemaining.Days > 0 Then
                                'sRemainingTime = spanRemaining.Days & "D "
                                sRemainingTime = spanRemaining.ToString("d\D\ hh\:mm\:ss")
                            Else
                                'sRemainingTime += spanRemaining.Hours.ToString("HH") & ":" & spanRemaining.Minutes.ToString("mm") & ":" & spanRemaining.Seconds.ToString("ss")
                                sRemainingTime = spanRemaining.ToString("hh\:mm\:ss")
                            End If
                            drEntry("StatusCol") = sRemainingTime
                            drEntry("TimeRemainingCol") = CInt(spanRemaining.TotalSeconds)
                        End If
                    Else
                        drEntry("StatusCol") = fGetStatus(Integer.Parse(objEntry.Status))
                        drEntry("TimeRemainingCol") = Integer.MaxValue
                    End If

                    drEntry("JobRunsCol") = "x " & objEntry.Runs.ToString
                    drEntry("ActivityCol") = ActivityIDLookup(objEntry.ActivityId)
                    drEntry("BlueprintCol") = objEntry.BueprintTypeName
                    drEntry("FacilityCol") = objEntry.SolarSystemName & " - " & fGetFacility(objEntry.FacilityId)
                    drEntry("InstallerCol") = objEntry.InstallerName
                    drEntry("InstallDateCol") = objEntry.StartDate.ToLocalTime
                    drEntry("EndDateCol") = objEntry.EndDate.ToLocalTime
                    drEntry("CharCorpCol") = "Char"


                    dtIndustry.Rows.Add(drEntry)

                Next
            End If

            If listCorpIndustryJobs.Result.Jobs IsNot Nothing Then

                For Each objEntry In listCorpIndustryJobs.Result.Jobs
                    drEntry = dtIndustry.NewRow

                    If fGetStatus(Integer.Parse(objEntry.Status)).Equals("Ready") Then
                        drEntry("StatusCol") = "Deliver"
                        drEntry("TimeRemainingCol") = 0
                    ElseIf fGetStatus(Integer.Parse(objEntry.Status)).Equals("Active") Then
                        spanRemaining = (objEntry.EndDate.ToLocalTime - Now)
                        If spanRemaining.TotalSeconds < 0 Then
                            drEntry("StatusCol") = "Deliver"
                            drEntry("TimeRemainingCol") = 0
                        Else
                            If spanRemaining.Days > 0 Then
                                'sRemainingTime = spanRemaining.Days & "D "
                                sRemainingTime = spanRemaining.ToString("d\D\ hh\:mm\:ss")
                            Else
                                'sRemainingTime += spanRemaining.Hours.ToString("HH") & ":" & spanRemaining.Minutes.ToString("mm") & ":" & spanRemaining.Seconds.ToString("ss")
                                sRemainingTime = spanRemaining.ToString("hh\:mm\:ss")
                            End If
                            drEntry("StatusCol") = sRemainingTime
                            drEntry("TimeRemainingCol") = CInt(spanRemaining.TotalSeconds)
                        End If
                    Else
                        drEntry("StatusCol") = fGetStatus(Integer.Parse(objEntry.Status))
                        drEntry("TimeRemainingCol") = Integer.MaxValue
                    End If

                    drEntry("JobRunsCol") = "x " & objEntry.Runs.ToString
                    drEntry("ActivityCol") = ActivityIDLookup(objEntry.ActivityId)
                    drEntry("BlueprintCol") = objEntry.BueprintTypeName
                    drEntry("FacilityCol") = objEntry.SolarSystemName & " - " & fGetFacility(objEntry.FacilityId)
                    drEntry("InstallerCol") = objEntry.InstallerName
                    drEntry("InstallDateCol") = objEntry.StartDate.ToLocalTime
                    drEntry("EndDateCol") = objEntry.EndDate.ToLocalTime
                    drEntry("CharCorpCol") = "Corp"

                    dtIndustry.Rows.Add(drEntry)

                Next
            End If

            dsIndustry.Tables.Add(dtIndustry)

            bsIndustry = New BindingSource

            bsIndustry.DataSource = dsIndustry
            bsIndustry.DataMember = dsIndustry.Tables(0).TableName

            dgvIndustry.DataSource = bsIndustry

            dgvIndustry.Visible = True

            'dgvIndustry.Sort(dgvIndustry.Columns("InstallDateCol"), System.ComponentModel.ListSortDirection.Descending)

            bsIndustry.Sort = "TimeRemainingCol ASC, InstallDateCol DESC"

            dgvIndustry.Columns("InstallDateCol").DefaultCellStyle.Format = "yyyy.MM.dd HH:mm"
            dgvIndustry.Columns("EndDateCol").DefaultCellStyle.Format = "yyyy.MM.dd HH:mm"

            dgvIndustry.Columns("StatusCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvIndustry.Columns("JobRunsCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvIndustry.Columns("ActivityCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvIndustry.Columns("BlueprintCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvIndustry.Columns("FacilityCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvIndustry.Columns("InstallerCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvIndustry.Columns("InstallDateCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvIndustry.Columns("EndDateCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            'dgvIndustry.Columns("TimeRemainingCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None

            dgvIndustry.Columns("StatusCol").Width = 75
            dgvIndustry.Columns("JobRunsCol").Width = 70
            dgvIndustry.Columns("ActivityCol").Width = 125
            dgvIndustry.Columns("BlueprintCol").Width = 250
            dgvIndustry.Columns("FacilityCol").Width = 250
            dgvIndustry.Columns("InstallerCol").Width = 150
            dgvIndustry.Columns("InstallDateCol").Width = 100
            dgvIndustry.Columns("EndDateCol").Width = 100
            'dgvIndustry.Columns("TimeRemainingCol").Width = 200

            dgvIndustry.Columns("StatusCol").HeaderText = "Status"
            dgvIndustry.Columns("JobRunsCol").HeaderText = "Job runs"
            dgvIndustry.Columns("ActivityCol").HeaderText = "Activity"
            dgvIndustry.Columns("BlueprintCol").HeaderText = "Blueprint"
            dgvIndustry.Columns("FacilityCol").HeaderText = "Facility"
            dgvIndustry.Columns("InstallerCol").HeaderText = "Installer"
            dgvIndustry.Columns("InstallDateCol").HeaderText = "Install Date"
            dgvIndustry.Columns("EndDateCol").HeaderText = "End Date"
            'dgvIndustry.Columns("TimeRemainingCol").HeaderText = "Remaining"

            dgvIndustry.Columns("TimeRemainingCol").Visible = False
            dgvIndustry.Columns("CharCorpCol").Visible = False

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function


    Private Async Function fLoadBlueprints() As task

        Dim objBlueprint As Models.Character.BlueprintList.Blueprint
        Dim drBlueprint As DataRow

        Dim StringType As System.Type = Type.GetType("System.String")
        Dim BooleanType As System.Type = Type.GetType("System.Boolean")
        Dim IntegerType As System.Type = Type.GetType("System.Int64")

        Try

            dsBlueprints = New DataSet
            dtBlueprints = New DataTable

            dtBlueprints.Columns.Add("BlueprintCol", StringType)
            dtBlueprints.Columns.Add("OriginalCol", BooleanType)
            dtBlueprints.Columns.Add("MECol", IntegerType)
            dtBlueprints.Columns.Add("TECol", StringType)
            dtBlueprints.Columns.Add("RunsRemainingCol", StringType)
            dtBlueprints.Columns.Add("CharCorpCol", StringType)
            dtBlueprints.Columns.Add("LocationCol", StringType)

            dtBlueprints.TableName = "Blueprints"

            For Each objBlueprint In listCharBlueprints.Result.Blueprints
                drBlueprint = dtBlueprints.NewRow

                drBlueprint("BlueprintCol") = objBlueprint.TypeName
                drBlueprint("MECol") = objBlueprint.MaterialEfficiency
                drBlueprint("TECol") = objBlueprint.TimeEfficiency.ToString & "%"
                If objBlueprint.Runs = -1 Then
                    drBlueprint("RunsRemainingCol") = Chr(165)
                    drBlueprint("OriginalCol") = True
                Else
                    drBlueprint("RunsRemainingCol") = objBlueprint.Runs.ToString
                    drBlueprint("OriginalCol") = False
                End If
                If dicAllAssets.ContainsKey(objBlueprint.ItemId) Then
                    drBlueprint("LocationCol") = dicAllAssets(objBlueprint.ItemId).Location & " - " & dicAllAssets(objBlueprint.ItemId).Container
                Else
                    drBlueprint("LocationCol") = "In Process - " & fGetFacility(objBlueprint.LocationId)
                End If
                drBlueprint("CharCorpCol") = "Character"

                dtBlueprints.Rows.Add(drBlueprint)
            Next

            For Each objBlueprint In listCorpBlueprints.Result.Blueprints
                drBlueprint = dtBlueprints.NewRow

                If objBlueprint.TypeName.Equals("Mining Laser Upgrade I Blueprint") Then
                    Debug.Print("Stop here")
                End If

                drBlueprint("BlueprintCol") = objBlueprint.TypeName
                drBlueprint("MECol") = objBlueprint.MaterialEfficiency
                drBlueprint("TECol") = objBlueprint.TimeEfficiency.ToString & "%"
                If objBlueprint.Runs = -1 Then
                    drBlueprint("RunsRemainingCol") = Chr(165)
                    drBlueprint("OriginalCol") = True
                Else
                    drBlueprint("RunsRemainingCol") = objBlueprint.Runs.ToString
                    drBlueprint("OriginalCol") = False
                End If
                If dicAllAssets.ContainsKey(objBlueprint.ItemId) Then
                    drBlueprint("LocationCol") = dicAllAssets(objBlueprint.ItemId).Location & " - " & dicAllAssets(objBlueprint.ItemId).Container
                Else
                    drBlueprint("LocationCol") = "In Process - " & fGetFacility(objBlueprint.LocationId)
                End If
                drBlueprint("CharCorpCol") = "Corporation"

                dtBlueprints.Rows.Add(drBlueprint)
            Next

            dsBlueprints.Tables.Add(dtBlueprints)

            bsBlueprints = New BindingSource

            bsBlueprints.DataSource = dsBlueprints
            bsBlueprints.DataMember = dsBlueprints.Tables(0).TableName

            dgvBlueprints.DataSource = bsBlueprints

            dgvBlueprints.Visible = True

            dgvBlueprints.Sort(dgvBlueprints.Columns("BlueprintCol"), System.ComponentModel.ListSortDirection.Ascending)


            dgvBlueprints.Columns("BlueprintCol").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            dgvBlueprints.Columns("MECol").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvBlueprints.Columns("TECol").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvBlueprints.Columns("RunsRemainingCol").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvBlueprints.Columns("LocationCol").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            dgvBlueprints.Columns("CharCorpCol").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvBlueprints.Columns("OriginalCol").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            dgvBlueprints.Columns("BlueprintCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvBlueprints.Columns("MECol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvBlueprints.Columns("TECol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvBlueprints.Columns("RunsRemainingCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvBlueprints.Columns("LocationCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            dgvBlueprints.Columns("CharCorpCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvBlueprints.Columns("OriginalCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None

            dgvBlueprints.Columns("BlueprintCol").Width = 250
            dgvBlueprints.Columns("MECol").Width = 75
            dgvBlueprints.Columns("TECol").Width = 75
            dgvBlueprints.Columns("RunsRemainingCol").Width = 75
            'dgvBlueprints.Columns("LocationCol").Width = 300
            dgvBlueprints.Columns("CharCorpCol").Width = 75
            dgvBlueprints.Columns("OriginalCol").Width = 50

            dgvBlueprints.Columns("BlueprintCol").HeaderText = "Blueprint"
            dgvBlueprints.Columns("MECol").HeaderText = "Material Efficiency"
            dgvBlueprints.Columns("TECol").HeaderText = "Time Efficiency"
            dgvBlueprints.Columns("RunsRemainingCol").HeaderText = "Runs remaining"
            dgvBlueprints.Columns("LocationCol").HeaderText = "Location"
            dgvBlueprints.Columns("CharCorpCol").HeaderText = "Char/Corp"
            dgvBlueprints.Columns("OriginalCol").HeaderText = "Original?"

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function


    Private Async Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Await fLoadEVEData()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        fUpdateSkillTimes(objSkillsInQueue)
    End Sub

    Private Sub dgvOrdersBuy_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvOrdersBuy.CellFormatting

        If e.RowIndex < 0 Then
            Exit Sub
        End If

        If dgvOrdersBuy.Rows(e.RowIndex).Cells("State").Value.ToString = GetOrderStateString(OrderState.Expired) Or _
            dgvOrdersBuy.Rows(e.RowIndex).Cells("State").Value.ToString = GetOrderStateString(OrderState.Closed) Then
            e.CellStyle.ForeColor = Color.Gray
        ElseIf dgvOrdersBuy.Rows(e.RowIndex).Cells("State").Value.ToString = GetOrderStateString(OrderState.Cancelled) Or _
            dgvOrdersBuy.Rows(e.RowIndex).Cells("State").Value.ToString = GetOrderStateString(OrderState.CharDeleted) Then
            e.CellStyle.ForeColor = Color.Red
        End If

        If dgvOrdersBuy.Columns(e.ColumnIndex).Name = "Issued" Then
            e.Value = fGetTimeString(Now() - CDate(e.Value))
        ElseIf dgvOrdersBuy.Columns(e.ColumnIndex).Name = "Expires" Then
            If (dgvOrdersBuy.Rows(e.RowIndex).Cells("State").Value.ToString = GetOrderStateString(OrderState.Open) Or _
                dgvOrdersBuy.Rows(e.RowIndex).Cells("State").Value.ToString = GetOrderStateString(OrderState.Pending)) Then
                e.Value = fGetTimeString(CDate(e.Value) - Now())
            Else
                e.Value = "-"
            End If
            'ElseIf dgvOrdersBuy.Columns(e.ColumnIndex).Name = "Range" Then
            '    If CInt(e.Value) = 0 Then
            '        e.Value = "System"
            '    ElseIf CInt(e.Value) = 32767 Then
            '        e.Value = "Station"
            '    ElseIf CInt(e.Value) = 65535 Then
            '        e.Value = "Region"
            '    End If
        End If

    End Sub

    Private Sub dgvOrdersSell_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvOrdersSell.CellFormatting

        If e.RowIndex < 0 Then
            Exit Sub
        End If

        If dgvOrdersSell.Rows(e.RowIndex).Cells("State").Value.ToString = GetOrderStateString(OrderState.Expired) Or _
            dgvOrdersSell.Rows(e.RowIndex).Cells("State").Value.ToString = GetOrderStateString(OrderState.Closed) Then
            e.CellStyle.ForeColor = Color.Gray
        ElseIf dgvOrdersSell.Rows(e.RowIndex).Cells("State").Value.ToString = GetOrderStateString(OrderState.Cancelled) Or _
            dgvOrdersSell.Rows(e.RowIndex).Cells("State").Value.ToString = GetOrderStateString(OrderState.CharDeleted) Then
            e.CellStyle.ForeColor = Color.Red
        End If

        If dgvOrdersSell.Columns(e.ColumnIndex).Name = "Issued" Then
            e.Value = fGetTimeString(Now() - CDate(e.Value))
        ElseIf dgvOrdersSell.Columns(e.ColumnIndex).Name = "Expires" Then
            If (dgvOrdersSell.Rows(e.RowIndex).Cells("State").Value.ToString = GetOrderStateString(OrderState.Open) Or _
                dgvOrdersSell.Rows(e.RowIndex).Cells("State").Value.ToString = GetOrderStateString(OrderState.Pending)) Then
                e.Value = fGetTimeString(CDate(e.Value) - Now())
            Else
                e.Value = "-"
            End If
        End If

    End Sub

    Private Sub dgvIndustry_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvIndustry.CellFormatting

        If e.RowIndex < 0 Then
            Exit Sub
        End If

        If dgvIndustry.Columns(e.ColumnIndex).Name = "StatusCol" Then
            If dgvIndustry.Rows(e.RowIndex).Cells("StatusCol").Value.ToString = "Deliver" Then
                e.CellStyle.BackColor = Color.LightGray
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            ElseIf dgvIndustry.Rows(e.RowIndex).Cells("StatusCol").Value.ToString.Contains(":") Then
                If dgvIndustry.Rows(e.RowIndex).Cells("ActivityCol").Value.ToString.Equals("Manufacturing") Then
                    e.CellStyle.ForeColor = Color.DarkGoldenrod
                Else
                    e.CellStyle.ForeColor = Color.DarkBlue
                End If
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            ElseIf dgvIndustry.Rows(e.RowIndex).Cells("StatusCol").Value.ToString = "Failed" Then
                e.CellStyle.ForeColor = Color.Red
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            ElseIf dgvIndustry.Rows(e.RowIndex).Cells("StatusCol").Value.ToString = "Delivered" Then
                e.CellStyle.ForeColor = Color.Green
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
        End If

            'If dgvIndustry.Columns(e.ColumnIndex).Name = "ActivityCol" Then
            '    If dgvIndustry.Rows(e.RowIndex).Cells("ActivityCol").Value.ToString = Product.Activity.ResearchMaterialProductivity.ToString Then
            '        e.Value = "Material Research"
            '    ElseIf dgvIndustry.Rows(e.RowIndex).Cells("ActivityCol").Value.ToString = Product.Activity.ResearchTimeProductivity.ToString Then
            '        e.Value = "Time Efficiency Research"
            '    End If
            'End If

    End Sub

    Private Sub cbActiveOrdersOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbActiveOrdersOnly.CheckedChanged

        If bsOrdersBuy IsNot Nothing And bsOrdersSell IsNot Nothing Then
            If cbActiveOrdersOnly.Checked = True Then
                bsOrdersSell.Filter = "State = 'Open'"
                bsOrdersBuy.Filter = "State = 'Open'"
            Else
                bsOrdersSell.Filter = ""
                bsOrdersBuy.Filter = ""
            End If
        End If

    End Sub

    Private Sub dgvJournal_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvJournal.CellFormatting

        If e.RowIndex < 0 Then
            Exit Sub
        End If

        If dgvJournal.Columns(e.ColumnIndex).Name = "AmountCol" Then
            If CDbl(dgvJournal.Rows(e.RowIndex).Cells("AmountCol").Value) < 0.0 Then
                e.CellStyle.ForeColor = Color.Red
            Else
                e.CellStyle.ForeColor = Color.Green
            End If
        End If

    End Sub

    Private Sub dgvTransactions_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvTransactions.CellFormatting

        If e.RowIndex < 0 Then
            Exit Sub
        End If

        If dgvTransactions.Columns(e.ColumnIndex).Name = "CreditCol" Then
            If CDbl(dgvTransactions.Rows(e.RowIndex).Cells("CreditCol").Value) < 0.0 Then
                e.CellStyle.ForeColor = Color.Red
            Else
                e.CellStyle.ForeColor = Color.Green
            End If
        End If

    End Sub


    Private Sub chkActiveOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActiveOnly.CheckedChanged
        If chkActiveOnly.Checked = True Then
            'bsIndustry.Filter = "StatusCol <> '" & fGetStatus(JobStatus.Cancelled) & "' AND " & _
            '                    "StatusCol <> '" & fGetStatus(JobStatus.Delivered) & "' AND " & _
            '                    "StatusCol <> '" & fGetStatus(JobStatus.Failed) & "' AND " & _
            '                    "StatusCol <> '" & fGetStatus(JobStatus.Paused) & "' AND " & _
            '                    "StatusCol <> '" & fGetStatus(JobStatus.Reverted) & "'"
            bsIndustry.Filter = "TimeRemainingCol < " & Integer.MaxValue
        Else
            bsIndustry.Filter = ""
        End If

    End Sub

    Private Sub dgvBlueprints_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvBlueprints.CellFormatting

        If e.RowIndex < 0 Then
            Exit Sub
        End If

        If dgvBlueprints.Columns(e.ColumnIndex).Name = "RunsRemainingCol" Then
            If CBool(dgvBlueprints.Rows(e.RowIndex).Cells("OriginalCol").Value) = True Then
                e.CellStyle.Font = New Font("Symbol", 12.0)
            End If
        End If

        If dgvBlueprints.Columns(e.ColumnIndex).Name = "MECol" Then
            e.Value = CStr(e.Value) & "%"
        End If

    End Sub


    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        frmAboutBox.ShowDialog()
    End Sub

    Private Sub APIInformationToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles APIInformationToolStripMenuItem.Click

        fDoAPIMenuClick()

    End Sub

    Private Sub fDoAPIMenuClick()

        Dim frmAPIInfo As frmAPI
        Dim objSettings As New CSettings(True)
        Dim retval As DialogResult

        frmAPIInfo = New frmAPI

        fGetAPIInfoFromFile()

        If IO.File.Exists(sAPIFilename & sAPIFileExt) Then

            frmAPIInfo.txtUserID.Text = iKeyID.ToString
            frmAPIInfo.txtFullAPIKey.Text = svCode
            frmAPIInfo.txtCharacterID.Text = lCharacterID.ToString

            frmAPIInfo.txtCorpKeyID.Text = iCorpKeyID.ToString
            frmAPIInfo.txtCorpvCode.Text = sCorpvCode
            frmAPIInfo.txtCorpCharID.Text = lCorpCharacterID.ToString

        End If

        retval = frmAPIInfo.ShowDialog()

        If retval = Windows.Forms.DialogResult.OK Then
            iKeyID = Integer.Parse(frmAPIInfo.txtUserID.Text)
            svCode = frmAPIInfo.txtFullAPIKey.Text
            lCharacterID = Long.Parse(frmAPIInfo.txtCharacterID.Text)

            iCorpKeyID = Integer.Parse(frmAPIInfo.txtCorpKeyID.Text)
            sCorpvCode = frmAPIInfo.txtCorpvCode.Text
            lCorpCharacterID = Long.Parse(frmAPIInfo.txtCorpCharID.Text)

            objSettings.SaveSetting(sAPIFilename, "Settings", "KeyID", iKeyID.ToString)
            objSettings.SaveSetting(sAPIFilename, "Settings", "vCode", svCode)
            objSettings.SaveSetting(sAPIFilename, "Settings", "CharacterID", lCharacterID.ToString)

            objSettings.SaveSetting(sAPIFilename, "Settings", "CorpKeyID", iCorpKeyID.ToString)
            objSettings.SaveSetting(sAPIFilename, "Settings", "CorpvCode", sCorpvCode)
            objSettings.SaveSetting(sAPIFilename, "Settings", "CorpCharacterID", lCorpCharacterID.ToString)

        End If

        frmAPIInfo = Nothing

    End Sub

    Private Sub fGetAPIInfoFromFile()

        Dim objSettings As New CSettings(True)

        If IO.File.Exists(sAPIFilename & sAPIFileExt) Then

            iKeyID = Integer.Parse(objSettings.GetSetting(sAPIFilename, "Settings", "KeyID", "0").ToString)
            svCode = CStr(objSettings.GetSetting(sAPIFilename, "Settings", "vCode", ""))
            lCharacterID = Long.Parse(objSettings.GetSetting(sAPIFilename, "Settings", "CharacterID", "0").ToString)

            iCorpKeyID = Integer.Parse(objSettings.GetSetting(sAPIFilename, "Settings", "CorpKeyID", "0").ToString)
            sCorpvCode = objSettings.GetSetting(sAPIFilename, "Settings", "CorpvCode", "").ToString
            lCorpCharacterID = Long.Parse(objSettings.GetSetting(sAPIFilename, "Settings", "CorpCharacterID", "0").ToString)

        End If

    End Sub

    Private Sub UpdateStatus(argForm As frmLoading, argMessage As String)

        argForm.SetText(argMessage)

    End Sub

    Private Async Function MakeMapsAsync() As task

        Dim objTasks As New List(Of Task)

        objTasks.Add(MakeSkillMapAsync())
        objTasks.Add(MakeItemMapAsync())
        objTasks.Add(MakeGroupMapAsync())
        objTasks.Add(MakeNameMapAsync())
        objTasks.Add(MakeCategoryMapAsync())
        objTasks.Add(MakeFlagMapAsync())

        Await Task.WhenAll(objTasks)

    End Function

    Private Function MakeSkillMapAsync() As task
        Return Task.Run(Sub() MakeSkillMap())
    End Function

    Private Sub MakeSkillMap()

        For Each objSkillGroup As Models.Misc.SkillTree.SkillGroup In objSkillTree.Result.Groups
            For Each objSkill As Models.Misc.SkillTree.Skill In objSkillGroup.Skills
                dicSkills.Add(objSkill.TypeId, objSkill)
            Next
        Next

    End Sub

    Private Function MakeItemMapAsync() As task
        Return Task.Run(Sub() MakeItemMap())
    End Function

    Private Sub MakeItemMap()

        'TYPEID	GROUPID	TYPENAME	DESCRIPTION	MASS	VOLUME	CAPACITY	PORTIONSIZE	RACEID	BASEPRICE	PUBLISHED	MARKETGROUPID	CHANCEOFDUPLICATING

        Dim currentRow As String()
        Dim objItem As EVEItem

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(sItemFilename)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(vbTab)
            ' Header
            currentRow = MyReader.ReadFields()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    objItem = New EVEItem(Integer.Parse(currentRow(0)))
                    objItem.GroupID = Integer.Parse(currentRow(1))
                    objItem.TypeName = currentRow(2)
                    objItem.Description = currentRow(3)

                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MessageBox.Show("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

    End Sub

    Private Function MakeGroupMapAsync() As task
        Return Task.Run(Sub() MakeGroupMap())
    End Function

    Private Sub MakeGroupMap()

        'GROUPID	CATEGORYID	GROUPNAME	DESCRIPTION	ICONID	USEBASEPRICE	ALLOWMANUFACTURE	ALLOWRECYCLER	ANCHORED	ANCHORABLE	FITTABLENONSINGLETON	PUBLISHED

        Dim currentRow As String()
        Dim objGroup As EVEGroup

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(sGroupFilename)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(vbTab)
            ' Header
            currentRow = MyReader.ReadFields()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    objGroup = New EVEGroup
                    objGroup.GroupID = Integer.Parse(currentRow(0))
                    objGroup.CategoryID = Integer.Parse(currentRow(1))
                    objGroup.GroupName = currentRow(2)

                    dicGroups.Add(objGroup.GroupID, objGroup)
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MessageBox.Show("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

    End Sub

    Private Function MakeNameMapAsync() As task
        Return Task.Run(Sub() MakeNameMap())
    End Function

    Private Sub MakeNameMap()
        '// locationID is a tricky one; for >= 66000000 and < 67000000 -> subtract 6000001
        '// for >= 67000000 and < 68000000 -> subtract 6000000
        '// and there's more:
        '// http://wiki.eve-id.net/APIv2_Corp_AssetList_XML
        'long loc = locationID;
        'if (locationID >= 66000000 && locationID < 66014933)
        '{
        'loc -= 6000001;
        '}
        'else if (locationID >= 66014934 && locationID < 67999999)
        '{
        'loc -= 6000000;
        '}

        'ITEMID	ITEMNAME

        Dim currentRow As String()
        Dim objName As EVEName

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(sNameFilename)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(vbTab)
            ' Header
            currentRow = MyReader.ReadFields()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    objName = New EVEName
                    objName.ItemID = Long.Parse(currentRow(0))
                    objName.ItemName = currentRow(1)

                    dicNames.Add(objName.ItemID, objName)
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MessageBox.Show("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

    End Sub

    Private Function MakeCategoryMapAsync() As task
        Return Task.Run(Sub() MakeCategoryMap())
    End Function

    Private Sub MakeCategoryMap()

        'CATEGORYID	CATEGORYNAME	DESCRIPTION	ICONID	PUBLISHED

        Dim currentRow As String()
        Dim objCategory As EVECategory

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(sCategoryFilename)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(vbTab)
            ' Header
            currentRow = MyReader.ReadFields()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    objCategory = New EVECategory
                    objCategory.CategoryID = Integer.Parse(currentRow(0))
                    objCategory.CategoryName = currentRow(1)

                    dicCategories.Add(objCategory.CategoryID, objCategory)
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MessageBox.Show("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

    End Sub

    Private Function MakeFlagMapAsync() As task
        Return Task.Run(Sub() MakeFlagMap())
    End Function

    Private Sub MakeFlagMap()

        'FLAGID	FLAGNAME	FLAGTEXT	ORDERID

        Dim currentRow As String()
        Dim objFlag As EVEFlag

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(sFlagFilename)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(vbTab)
            ' Header
            currentRow = MyReader.ReadFields()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    objFlag = New EVEFlag
                    objFlag.FlagID = Integer.Parse(currentRow(0))
                    objFlag.FlagName = currentRow(1)
                    objFlag.FlagText = currentRow(2)

                    dicFlags.Add(objFlag.FlagID, objFlag)
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MessageBox.Show("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

    End Sub

    Private Function NameLookup(iLocationID As Long) As String

        If iLocationID >= 66000000 And iLocationID < 66014933 Then
            Return dicNames(iLocationID - 6000001).ItemName
        ElseIf iLocationID >= 66014934 And iLocationID < 67999999 Then
            Return dicNames(iLocationID - 6000000).ItemName
        Else
            If dicNames.ContainsKey(iLocationID) Then
                Return dicNames(iLocationID).ItemName
            Else
                Return ""
            End If
        End If

    End Function

    Private Function ActivityIDLookup(argActivityID As Integer) As String

        Select Case argActivityID
            Case ActivityID.Manufacturing
                Return "Manufacturing"
            Case ActivityID.Copying
                Return "Copying"
            Case ActivityID.Duplicating
                Return "Duplicating"
            Case ActivityID.Invention
                Return "Invention"
            Case ActivityID.Researching_Material_Productivity
                Return "Researching Material Productivity"
            Case ActivityID.Researching_Technology
                Return "Researching Technology"
            Case ActivityID.Researching_Time_Productivity
                Return "Researching Time Productivity"
            Case ActivityID.Reverse_Engineering
                Return "Reverse Engineering"
            Case Else
                Return "Unknown"
        End Select

    End Function

    Private Function fGetStatus(argStatus As Integer) As String
        Select Case argStatus
            Case JobStatus.Active
                Return JobStatus.Active.ToString
            Case JobStatus.Paused
                Return JobStatus.Paused.ToString
            Case JobStatus.Ready
                Return JobStatus.Ready.ToString
            Case JobStatus.Cancelled
                Return JobStatus.Cancelled.ToString
            Case JobStatus.Reverted
                Return JobStatus.Reverted.ToString
            Case JobStatus.Delivered
                Return JobStatus.Delivered.ToString
            Case JobStatus.Failed
                Return JobStatus.Failed.ToString
            Case Else
                Return "Unknown"
        End Select
    End Function


    Private Function fGetFacility(argFacility As Long) As String

        For Each objFacility As Models.Corporation.Facilities.Facility In listCorpFacilities.Result.FacilityEntries
            If objFacility.FacilityId = argFacility Then
                Return EVEItem.Find(objFacility.TypeId).TypeName
            End If
        Next

        Return NameLookup(argFacility)

    End Function

End Class
