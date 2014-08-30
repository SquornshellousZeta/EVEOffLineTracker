Public Class EVEItem

    'TYPEID	GROUPID	TYPENAME	DESCRIPTION	MASS	VOLUME	CAPACITY	PORTIONSIZE	RACEID	BASEPRICE	PUBLISHED	MARKETGROUPID	CHANCEOFDUPLICATING

    Private Shared dicItems As New Dictionary(Of Integer, EVEItem)

    Sub New(argTypeID As Integer)
        dicItems.Add(argTypeID, Me)
    End Sub

    Shared Function Find(argItemID As Integer) As EVEItem
        If dicItems.ContainsKey(argItemID) Then
            Return dicItems(argItemID)
        End If

        Return Nothing
    End Function

    Private iTypeID As Integer
    Public Property TypeID() As Integer
        Get
            Return iTypeID
        End Get
        Set(ByVal value As Integer)
            iTypeID = value
        End Set
    End Property

    Private iGroupID As Integer
    Public Property GroupID() As Integer
        Get
            Return iGroupID
        End Get
        Set(ByVal value As Integer)
            iGroupID = value
        End Set
    End Property

    Private sTypeName As String
    Public Property TypeName() As String
        Get
            Return sTypeName
        End Get
        Set(ByVal value As String)
            sTypeName = value
        End Set
    End Property

    Private sDescription As String
    Public Property Description() As String
        Get
            Return sDescription
        End Get
        Set(ByVal value As String)
            sDescription = value
        End Set
    End Property



End Class
