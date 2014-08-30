Public Class EVEGroup

    'GROUPID	CATEGORYID	GROUPNAME	DESCRIPTION	ICONID	USEBASEPRICE	ALLOWMANUFACTURE	ALLOWRECYCLER	ANCHORED	ANCHORABLE	FITTABLENONSINGLETON	PUBLISHED

    Private iGroupID As Integer
    Public Property GroupID() As Integer
        Get
            Return iGroupID
        End Get
        Set(ByVal value As Integer)
            iGroupID = value
        End Set
    End Property

    Private iCategoryID As Integer
    Public Property CategoryID() As Integer
        Get
            Return iCategoryID
        End Get
        Set(ByVal value As Integer)
            iCategoryID = value
        End Set
    End Property

    Private sGropupName As String
    Public Property GroupName() As String
        Get
            Return sGropupName
        End Get
        Set(ByVal value As String)
            sGropupName = value
        End Set
    End Property

End Class
