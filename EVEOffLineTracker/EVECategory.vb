Public Class EVECategory

    'CATEGORYID	CATEGORYNAME	DESCRIPTION	ICONID	PUBLISHED

    Private iCategoryID As Integer
    Public Property CategoryID() As Integer
        Get
            Return iCategoryID
        End Get
        Set(ByVal value As Integer)
            iCategoryID = value
        End Set
    End Property

    Private sCategoryName As String
    Public Property CategoryName() As String
        Get
            Return sCategoryName
        End Get
        Set(ByVal value As String)
            sCategoryName = value
        End Set
    End Property

End Class
