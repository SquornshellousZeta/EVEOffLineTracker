Imports eZet.EveLib.Modules
Imports eZet.EveLib.Modules.EveOnlineApi

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
    'Private listCharIndustryJobs As List(Of IndustryJob)
    'Private listCorpIndustryJobs As List(Of IndustryJob)
    'Private listNotifications As List(Of Character.Notification)

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

    Private dsNotifications As DataSet
    Private dtNotifications As DataTable
    Private bsNotifications As BindingSource

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

    Private Async Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Await fLoadEVEData()

        Timer1.Enabled = True

    End Sub

    Private Async Function fLoadEVEData() As Threading.Tasks.Task

        Dim frm As frmLoading

        sListOfErrors = ""

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

        MakeSkillMap()

        MakeItemMap()

        MakeGroupMap()

        MakeNameMap()

        MakeCategoryMap()

        MakeFlagMap()

        UpdateStatus(frm, "Getting API information")
        character = New Character(iKeyID, svCode, lCharacterID)

        If iCorpKeyID > 0 And sCorpvCode <> "" And lCorpCharacterID > 0 Then
            corporation = New Corporation(iCorpKeyID, sCorpvCode, lCorpCharacterID)
            UpdateStatus(frm, "Getting Corp API information")
        End If

        Try

            UpdateStatus(frm, "Loading Character Sheet...")
            charSheetResponse = character.GetCharacterSheet()

            Me.tsslCharSheet.Text = "Cached Until: " & charSheetResponse.CachedUntilAsString

            Await fLoadCharacterSheet(charSheetResponse.Result)


        Catch ex As Exception
            sListOfErrors += "Failed to get Character Sheet data" + vbLf
        End Try


        Try

            UpdateStatus(frm, "Loading Character Assets...")
            listAssets = character.GetAssetList()
            Me.tsslAssets.Text = "Cached Until: " & listAssets.CachedUntilAsString

        Catch ex As Exception
            sListOfErrors += "Failed to get Character Asset data" + vbLf
        End Try

        If Not IsNothing(corporation) Then

            Try

                UpdateStatus(frm, "Loading Corporation Assets...")
                listCorpAssets = corporation.GetAssetList()
                Me.tsslAssets.Text = "Cached Until: " & listCorpAssets.CachedUntilAsString

            Catch ex As Exception
                sListOfErrors += "Failed to get Corporation Asset data" + vbLf
            End Try
        End If

        fLoadAssets()

        Try

            UpdateStatus(frm, "Loading Market Orders...")
            listOrders = Await character.GetMarketOrdersAsync()
            Me.tsslOrders.Text = "Cached Until: " & listOrders.CachedUntilAsString

            If corporation IsNot Nothing Then
                listCorpOrders = Await corporation.GetMarketOrdersAsync()
            End If

            fLoadMarketOrders()
            cbActiveOrdersOnly.Checked = False
            cbActiveOrdersOnly.Checked = True
        Catch ex As Exception
            sListOfErrors += "Failed to get Market Order data" + vbLf
        End Try

        Try
            UpdateStatus(frm, "Loading Wallet Journal...")
            listJournal = Await character.GetWalletJournalAsync(2560)
            Me.tsslJournal.Text = "Cached Until: " & listJournal.CachedUntilAsString

            fLoadJournal()
        Catch ex As Exception
            sListOfErrors += "Failed to get Wallet Journal data" + vbLf
        End Try

        Try
            UpdateStatus(frm, "Loading Wallet Transactions...")
            listTransactions = Await character.GetWalletTransactionsAsync(2560)
            Me.tsslTransactions.Text = "Cached Until: " & listTransactions.CachedUntilAsString

            fLoadTransactions()
        Catch ex As Exception
            sListOfErrors += "Failed to get Wallet Transaction data" + vbLf
        End Try

        'Try

        '    frm.lblText.Text = "Loading Industry Jobs..."
        '    Application.DoEvents()
        '    listCharIndustryJobs = api.GetCharacterIndustryJobs
        '    isApiOk("GetCharacterIndustryJobs", api.LastErrors)
        '    If apiCorp IsNot Nothing Then
        '        listCorpIndustryJobs = apiCorp.GetCorporationIndustryJobs
        '        isApiOk("GetCorporationIndustryJobs", apiCorp.LastErrors)
        '    End If
        '    Me.tsslIndustry.Text = "Cached Until: " & api.LastQueryCachedUntil.ToLongDateString & " " & _
        '                                    api.LastQueryCachedUntil.ToLongTimeString

        '    fLoadIndustry()
        'Catch ex As Exception
        '    sListOfErrors += "Failed to get Industry Job data" + vbLf
        'End Try

        'Try

        '    frm.lblText.Text = "Loading Notifications..."
        '    Application.DoEvents()
        '    listNotifications = api.GetCharacterNotifications
        '    isApiOk("GetCharacterNotifications", api.LastErrors)
        '    Me.tsslNotifications.Text = "Cached Until: " & api.LastQueryCachedUntil.ToLongDateString & " " & _
        '                                    api.LastQueryCachedUntil.ToLongTimeString

        '    fLoadNotifications()
        'Catch ex As Exception
        '    sListOfErrors += "Failed to get Notification data" + vbLf
        'End Try

        'api.LastQueryCachedUntil.ToLongTimeString()

        frm.Close()

        'If sListOfErrors.Trim.Length > 0 Then
        '    MessageBox.Show(frmLoading, sListOfErrors, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End If

    End Function

    Private Async Function fLoadCharacterSheet(charSheet As Models.Character.CharacterSheet) As Threading.Tasks.Task

        Dim iTotalSkillPoints As Integer
        Dim iIntEnhanced, iMemEnhanced, iPerEnhanced, iWilEnhanced, iChaEnhanced As Integer
        Dim objImage As Image
        Dim sImageFile As String

        Try

            iTotalSkillPoints = GetTotalSkillPoints(charSheet.Skills)

            Me.lblName.Text = charSheet.Name
            sCharacterName = charSheet.Name
            Me.lblRace.Text = charSheet.Race
            Me.lblCorporation.Text = charSheet.CorporationName
            Me.lblGender.Text = charSheet.Gender
            Me.lblSkillpoints.Text = String.Format("{0:#,0}", iTotalSkillPoints)
            Me.lblCloneInfo.Text = String.Format("{0:#,0}", charSheet.CloneSkillPoints) & "  (" & charSheet.CloneName & ")"
            If iTotalSkillPoints > charSheet.CloneSkillPoints Then
                Me.lblSkillpoints.ForeColor = Color.Red
            End If
            Me.lblBalance.Text = String.Format("{0:#,#.00 ISK}", charSheet.Balance)

            If (charSheet.AttributeEnhancers.Intelligence IsNot Nothing) Then
                iIntEnhanced = charSheet.AttributeEnhancers.Intelligence.Value
                Me.lblIntImplant.Text = "+" & CStr(iIntEnhanced)
            Else
                Me.lblIntImplant.Text = "-"
            End If
            If (charSheet.AttributeEnhancers.Memory IsNot Nothing) Then
                iMemEnhanced = charSheet.AttributeEnhancers.Memory.Value
                Me.lblMemImplant.Text = "+" & CStr(iMemEnhanced)
            Else
                Me.lblMemImplant.Text = "-"
            End If
            If (charSheet.AttributeEnhancers.Perception IsNot Nothing) Then
                iPerEnhanced = charSheet.AttributeEnhancers.Perception.Value
                Me.lblPerImplant.Text = "+" & CStr(iPerEnhanced)
            Else
                Me.lblPerImplant.Text = "-"
            End If
            If (charSheet.AttributeEnhancers.Willpower IsNot Nothing) Then
                iWilEnhanced = charSheet.AttributeEnhancers.Willpower.Value
                Me.lblWillImplant.Text = "+" & CStr(iWilEnhanced)
            Else
                Me.lblWillImplant.Text = "-"
            End If
            If (charSheet.AttributeEnhancers.Charisma IsNot Nothing) Then
                iChaEnhanced = charSheet.AttributeEnhancers.Charisma.Value
                Me.lblChaImplant.Text = "+" & CStr(iChaEnhanced)
            Else
                Me.lblChaImplant.Text = "-"
            End If

            Me.lblInt.Text = String.Format("{0:#.00}", charSheet.Attributes.Intelligence + iIntEnhanced) & "  (" & _
                            String.Format("{0:#}", charSheet.Attributes.Intelligence) & ")"
            Me.lblMem.Text = String.Format("{0:#.00}", charSheet.Attributes.Memory + iMemEnhanced) & "  (" & _
                            String.Format("{0:#}", charSheet.Attributes.Memory) & ")"
            Me.lblPer.Text = String.Format("{0:#.00}", charSheet.Attributes.Perception + iPerEnhanced) & "  (" & _
                            String.Format("{0:#}", charSheet.Attributes.Perception) & ")"
            Me.lblWill.Text = String.Format("{0:#.00}", charSheet.Attributes.Willpower + iWilEnhanced) & "  (" & _
                            String.Format("{0:#}", charSheet.Attributes.Willpower) & ")"
            Me.lblCha.Text = String.Format("{0:#.00}", charSheet.Attributes.Charisma + iChaEnhanced) & "  (" & _
                            String.Format("{0:#}", charSheet.Attributes.Charisma) & ")"

            objImage = New Image()

            Await objImage.GetCharacterPortraitAsync(lCharacterID, eZet.EveLib.Modules.Image.CharacterPortraitSize.X256, "D:\\temp")
            sImageFile = IO.Path.Combine("D:\temp", lCharacterID.ToString & "_" & eZet.EveLib.Modules.Image.CharacterPortraitSize.X256 & ".jpg")
            Me.picImage.Image = System.Drawing.Image.FromFile(sImageFile)

            Await objImage.GetCorporationLogoAsync(charSheet.CorporationId, eZet.EveLib.Modules.Image.CorporationLogoSize.X64, "D:\\temp")
            sImageFile = IO.Path.Combine("D:\temp", charSheet.CorporationId.ToString & "_" & eZet.EveLib.Modules.Image.CorporationLogoSize.X64 & ".png")
            Me.imgCorp.Image = System.Drawing.Image.FromFile(sImageFile)

            If charSheet.AllianceId > 0 Then
                Await objImage.GetAllianceLogoAsync(charSheet.AllianceId, eZet.EveLib.Modules.Image.AllianceLogoSize.X64, "D:\\temp")
                sImageFile = IO.Path.Combine("D:\temp", lCharacterID.ToString & "_" & eZet.EveLib.Modules.Image.AllianceLogoSize.X64 & ".png")
                Me.imgAlliance.Image = System.Drawing.Image.FromFile(sImageFile)
            End If

            'Me.imgCorp.Image = EveAI.Live.ImageServer.DownloadCorporationImage(charSheet.CorporationID, ImageServer.ImageSize.Size64px)
            ' Me.imgAlliance.Image = EveAI.Live.ImageServer.DownloadAllianceImage(api.GetCorporationSheet.AllianceID, ImageServer.ImageSize.Size64px)

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


    End Function

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

        lbSkillQueue.Items.Clear()
        lbSkillLevel.Items.Clear()
        lbSkillTimeRemaining.Items.Clear()

        If argSkillsInQueue IsNot Nothing Then
            For Each objSkill In argSkillsInQueue.Queue
                lbSkillQueue.Items.Add(dicSkills(objSkill.TypeId).TypeName)
                lbSkillLevel.Items.Add(objSkill.Level)
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
                lbSkillTimeRemaining.Items.Add(sRemaining)
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

    Private Sub fLoadAssets()

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

    End Sub

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

    Private Sub fLoadMarketOrders()

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

    End Sub

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

    Private Sub fLoadJournal()

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

    End Sub

    Private Sub fLoadTransactions()

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
                drEntry("CreditCol") = objEntry.Price
            End If
            drEntry("EntryTimeCol") = objEntry.TransactionDate
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

    End Sub


    'Private Sub fLoadIndustry()

    '    Dim objEntry As IndustryJob
    '    Dim drEntry As DataRow

    '    Dim StringType As System.Type = Type.GetType("System.String")
    '    Dim DateType As System.Type = Type.GetType("System.DateTime")
    '    Dim IntegerType As System.Type = Type.GetType("System.Int64")

    '    Dim sLoc As String
    '    Dim spanRemaining As TimeSpan

    '    dsIndustry = New DataSet
    '    dtIndustry = New DataTable

    '    dtIndustry.Columns.Add("StateCol", StringType)
    '    dtIndustry.Columns.Add("ActivityCol", StringType)
    '    dtIndustry.Columns.Add("TypeCol", StringType)
    '    dtIndustry.Columns.Add("LocationCol", StringType)
    '    dtIndustry.Columns.Add("JumpsCol", IntegerType)
    '    dtIndustry.Columns.Add("InstallerCol", StringType)
    '    dtIndustry.Columns.Add("OwnerCol", StringType)
    '    dtIndustry.Columns.Add("InstallDateCol", DateType)
    '    dtIndustry.Columns.Add("EndDateCol", DateType)
    '    dtIndustry.Columns.Add("TimeRemainingCol", StringType)

    '    dtIndustry.TableName = "IndustryJobs"

    '    Dim lookup As New EveAI.Live.Generic.CharacterNameLookupApi()
    '    Dim sCorpName As String = api.GetCharacterSheet.CorporationName

    '    If listCharIndustryJobs IsNot Nothing Then
    '        For Each objEntry In listCharIndustryJobs
    '            If Not lookup.CharacterIDsToLookup.Contains(objEntry.InstallerID) Then
    '                lookup.CharacterIDsToLookup.Add(objEntry.InstallerID)
    '            End If
    '        Next
    '    End If

    '    If listCorpIndustryJobs IsNot Nothing Then
    '        For Each objEntry In listCorpIndustryJobs
    '            If Not lookup.CharacterIDsToLookup.Contains(objEntry.InstallerID) Then
    '                lookup.CharacterIDsToLookup.Add(objEntry.InstallerID)
    '            End If
    '        Next
    '    End If

    '    lookup.UpdateData()

    '    If listCharIndustryJobs IsNot Nothing Then

    '        For Each objEntry In listCharIndustryJobs
    '            drEntry = dtIndustry.NewRow

    '            If objEntry.IsCompleted = False Then
    '                If Now > objEntry.ProductionEndLocalTime Then
    '                    drEntry("StateCol") = "Ready"
    '                Else
    '                    If Now < objEntry.ProductionBeginLocalTime Then
    '                        drEntry("StateCol") = "Pending"
    '                    Else
    '                        drEntry("StateCol") = "In Progress"
    '                    End If
    '                End If

    '                If Now < objEntry.ProductionEndLocalTime Then
    '                    spanRemaining = objEntry.ProductionEndLocalTime - Now
    '                Else
    '                    spanRemaining = TimeSpan.Zero
    '                End If

    '                drEntry("ActivityCol") = objEntry.Activity.ToString
    '                drEntry("TypeCol") = objEntry.InstalledItem.ToString
    '                drEntry("LocationCol") = objEntry.InstallLocation.Container.Name
    '                drEntry("JumpsCol") = 0 'objEntry.InstallLocation.ContainerLocation.Jumps
    '                drEntry("InstallerCol") = lookup.FindEntry(objEntry.InstallerID)
    '                drEntry("OwnerCol") = drEntry("InstallerCol")
    '                drEntry("InstallDateCol") = objEntry.InstallTimeLocalTime
    '                drEntry("EndDateCol") = objEntry.ProductionEndLocalTime
    '                drEntry("TimeRemainingCol") = fGetTimeString(spanRemaining)

    '                dtIndustry.Rows.Add(drEntry)
    '            End If

    '        Next
    '    End If

    '    If listCorpIndustryJobs IsNot Nothing Then

    '        For Each objEntry In listCorpIndustryJobs
    '            drEntry = dtIndustry.NewRow

    '            If objEntry.IsCompleted = False Then
    '                If Now > objEntry.ProductionEndLocalTime Then
    '                    drEntry("StateCol") = "Ready"
    '                Else
    '                    If Now < objEntry.ProductionBeginLocalTime Then
    '                        drEntry("StateCol") = "Pending"
    '                    Else
    '                        drEntry("StateCol") = "In Progress"
    '                    End If
    '                End If

    '                If objEntry.InstallLocation.Container Is Nothing Then
    '                    sLoc = objEntry.InstallLocation.ContainerType.Name & " in " & objEntry.InstallLocation.ContainerLocation.Name
    '                Else
    '                    sLoc = objEntry.InstallLocation.Container.Name
    '                End If

    '                If Now < objEntry.ProductionEndLocalTime Then
    '                    spanRemaining = objEntry.ProductionEndLocalTime - Now
    '                Else
    '                    spanRemaining = TimeSpan.Zero
    '                End If

    '                drEntry("ActivityCol") = objEntry.Activity.ToString
    '                drEntry("TypeCol") = objEntry.InstalledItem.ToString
    '                drEntry("LocationCol") = sLoc
    '                drEntry("JumpsCol") = 0 'objEntry.InstallLocation.ContainerLocation.Jumps
    '                drEntry("InstallerCol") = lookup.FindEntry(objEntry.InstallerID)
    '                drEntry("OwnerCol") = sCorpName
    '                drEntry("InstallDateCol") = objEntry.InstallTimeLocalTime
    '                drEntry("EndDateCol") = objEntry.ProductionEndLocalTime
    '                drEntry("TimeRemainingCol") = fGetTimeString(spanRemaining)

    '                dtIndustry.Rows.Add(drEntry)
    '            End If

    '        Next
    '    End If

    '    dsIndustry.Tables.Add(dtIndustry)

    '    bsIndustry = New BindingSource

    '    bsIndustry.DataSource = dsIndustry
    '    bsIndustry.DataMember = dsIndustry.Tables(0).TableName

    '    dgvIndustry.DataSource = bsIndustry

    '    dgvIndustry.Visible = True

    '    dgvIndustry.Sort(dgvIndustry.Columns("InstallDateCol"), System.ComponentModel.ListSortDirection.Descending)

    '    dgvIndustry.Columns("InstallDateCol").DefaultCellStyle.Format = "yyyy.MM.dd HH:mm"
    '    dgvIndustry.Columns("EndDateCol").DefaultCellStyle.Format = "yyyy.MM.dd HH:mm"

    '    dgvIndustry.Columns("StateCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
    '    dgvIndustry.Columns("ActivityCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
    '    dgvIndustry.Columns("TypeCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
    '    dgvIndustry.Columns("LocationCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
    '    dgvIndustry.Columns("JumpsCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
    '    dgvIndustry.Columns("InstallerCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
    '    dgvIndustry.Columns("OwnerCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
    '    dgvIndustry.Columns("InstallDateCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
    '    dgvIndustry.Columns("EndDateCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
    '    dgvIndustry.Columns("TimeRemainingCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None

    '    dgvIndustry.Columns("StateCol").Width = 100
    '    dgvIndustry.Columns("ActivityCol").Width = 125
    '    dgvIndustry.Columns("TypeCol").Width = 200
    '    dgvIndustry.Columns("JumpsCol").Width = 50
    '    dgvIndustry.Columns("LocationCol").Width = 250
    '    dgvIndustry.Columns("InstallerCol").Width = 150
    '    dgvIndustry.Columns("OwnerCol").Width = 150
    '    dgvIndustry.Columns("InstallDateCol").Width = 100
    '    dgvIndustry.Columns("EndDateCol").Width = 100
    '    dgvIndustry.Columns("TimeRemainingCol").Width = 200

    '    dgvIndustry.Columns("StateCol").HeaderText = "State"
    '    dgvIndustry.Columns("ActivityCol").HeaderText = "Activity"
    '    dgvIndustry.Columns("TypeCol").HeaderText = "Type"
    '    dgvIndustry.Columns("LocationCol").HeaderText = "Location"
    '    dgvIndustry.Columns("JumpsCol").HeaderText = "Jumps"
    '    dgvIndustry.Columns("InstallerCol").HeaderText = "Installer"
    '    dgvIndustry.Columns("OwnerCol").HeaderText = "Owner"
    '    dgvIndustry.Columns("InstallDateCol").HeaderText = "Install Date"
    '    dgvIndustry.Columns("EndDateCol").HeaderText = "End Date"
    '    dgvIndustry.Columns("TimeRemainingCol").HeaderText = "Remaining"

    'End Sub

    'Private Sub fLoadNotifications()

    '    Dim objNotification As Character.Notification
    '    Dim drNotification As DataRow

    '    Dim StringType As System.Type = Type.GetType("System.String")
    '    Dim DateType As System.Type = Type.GetType("System.DateTime")
    '    Dim BooleanType As System.Type = Type.GetType("System.Boolean")

    '    Dim sName As String
    '    Dim objAgent As EveAI.Npc.Agent
    '    Dim objCorp As EveAI.Npc.NpcCorporation

    '    dsNotifications = New DataSet
    '    dtNotifications = New DataTable

    '    dtNotifications.Columns.Add("ReadCol", BooleanType)
    '    dtNotifications.Columns.Add("SenderCol", StringType)
    '    dtNotifications.Columns.Add("SubjectCol", StringType)
    '    dtNotifications.Columns.Add("ReceivedTimeCol", DateType)

    '    dtNotifications.TableName = "Notifications"

    '    For Each objNotification In listNotifications
    '        drNotification = dtNotifications.NewRow

    '        objAgent = api.EveApiCore.FindAgent(CInt(objNotification.SenderID))
    '        If objAgent Is Nothing Then
    '            objCorp = api.EveApiCore.FindNpcCorporation(CInt(objNotification.SenderID))
    '            If objCorp Is Nothing Then
    '                If objNotification.SenderID = 2 Then
    '                    sName = "EVE Central Bank"
    '                Else
    '                    sName = "Unknown"
    '                End If
    '            Else
    '                sName = objCorp.Name
    '            End If
    '        Else
    '            sName = objAgent.Name
    '        End If

    '        drNotification("ReadCol") = objNotification.IsRead
    '        drNotification("SenderCol") = sName
    '        drNotification("SubjectCol") = objNotification.Type.ToString
    '        drNotification("ReceivedTimeCol") = objNotification.SentDateLocalTime
    '        dtNotifications.Rows.Add(drNotification)
    '    Next

    '    dsNotifications.Tables.Add(dtNotifications)

    '    bsNotifications = New BindingSource

    '    bsNotifications.DataSource = dsNotifications
    '    bsNotifications.DataMember = dsNotifications.Tables(0).TableName

    '    dgvNotifications.DataSource = bsNotifications

    '    dgvNotifications.Visible = True

    '    dgvNotifications.Sort(dgvNotifications.Columns("ReceivedTimeCol"), System.ComponentModel.ListSortDirection.Descending)

    '    dgvNotifications.Columns("ReadCol").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '    dgvNotifications.Columns("SenderCol").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
    '    dgvNotifications.Columns("SubjectCol").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
    '    dgvNotifications.Columns("ReceivedTimeCol").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

    '    dgvNotifications.Columns("ReceivedTimeCol").DefaultCellStyle.Format = "yyyy.MM.dd HH:mm"

    '    dgvNotifications.Columns("ReadCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
    '    dgvNotifications.Columns("SenderCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
    '    dgvNotifications.Columns("SubjectCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    '    dgvNotifications.Columns("ReceivedTimeCol").AutoSizeMode = DataGridViewAutoSizeColumnMode.None

    '    dgvNotifications.Columns("ReadCol").Width = 50
    '    dgvNotifications.Columns("SenderCol").Width = 150
    '    dgvNotifications.Columns("ReceivedTimeCol").Width = 125

    '    dgvNotifications.Columns("ReadCol").HeaderText = "Read?"
    '    dgvNotifications.Columns("SenderCol").HeaderText = "Sender"
    '    dgvNotifications.Columns("SubjectCol").HeaderText = "Subject"
    '    dgvNotifications.Columns("ReceivedTimeCol").HeaderText = "Received"

    'End Sub


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

    'Private Sub dgvIndustry_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvIndustry.CellFormatting

    '    If e.RowIndex < 0 Then
    '        Exit Sub
    '    End If

    '    If dgvIndustry.Columns(e.ColumnIndex).Name = "StateCol" Then
    '        If dgvIndustry.Rows(e.RowIndex).Cells("StateCol").Value.ToString = "Ready" Then
    '            e.CellStyle.ForeColor = Color.Green
    '        ElseIf dgvIndustry.Rows(e.RowIndex).Cells("StateCol").Value.ToString = "In Progress" Then
    '            e.CellStyle.ForeColor = Color.Gold
    '        ElseIf dgvIndustry.Rows(e.RowIndex).Cells("StateCol").Value.ToString = "Pending" Then
    '            e.CellStyle.ForeColor = Color.Red
    '        End If
    '    End If

    '    If dgvIndustry.Columns(e.ColumnIndex).Name = "ActivityCol" Then
    '        If dgvIndustry.Rows(e.RowIndex).Cells("ActivityCol").Value.ToString = Product.Activity.ResearchMaterialProductivity.ToString Then
    '            e.Value = "Material Research"
    '        ElseIf dgvIndustry.Rows(e.RowIndex).Cells("ActivityCol").Value.ToString = Product.Activity.ResearchTimeProductivity.ToString Then
    '            e.Value = "Time Efficiency Research"
    '        End If
    '    End If

    'End Sub

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


    Private Sub chkMeOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMeOnly.CheckedChanged
        If chkMeOnly.Checked = True Then
            bsIndustry.Filter = "InstallerCol = '" & sCharacterName & "'"
        Else
            bsIndustry.Filter = ""
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

    Private Sub MakeSkillMap()

        For Each objSkillGroup As Models.Misc.SkillTree.SkillGroup In objSkillTree.Result.Groups
            For Each objSkill As Models.Misc.SkillTree.Skill In objSkillGroup.Skills
                dicSkills.Add(objSkill.TypeId, objSkill)
            Next
        Next

    End Sub


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

    Private Function NameLookup(iLocationID As Long) As String

        If iLocationID >= 66000000 And iLocationID < 66014933 Then
            Return dicNames(iLocationID - 6000001).ItemName
        ElseIf iLocationID >= 66014934 And iLocationID < 67999999 Then
            Return dicNames(iLocationID - 6000000).ItemName
        Else
            Return dicNames(iLocationID).ItemName
        End If

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

End Class
