Public Class Asset

    Private iItemID As Long
    Public Property ItemID() As Long
        Get
            Return iItemID
        End Get
        Set(ByVal value As Long)
            iItemID = value
        End Set
    End Property

    Private iTypeID As Integer
    Public Property TypeID() As Integer
        Get
            Return iTypeID
        End Get
        Set(ByVal value As Integer)
            iTypeID = value
        End Set
    End Property

    Private sLocation As String
    Public Property Location() As String
        Get
            Return sLocation
        End Get
        Set(ByVal value As String)
            sLocation = value
        End Set
    End Property

    Private sContainer As String
    Public Property Container() As String
        Get
            Return sContainer
        End Get
        Set(ByVal value As String)
            sContainer = value
        End Set
    End Property

    Private iQuantity As Integer
    Public Property Quantity() As Integer
        Get
            Return iQuantity
        End Get
        Set(ByVal value As Integer)
            iQuantity = value
        End Set
    End Property

    Private bIsCharAsset As Boolean
    Public Property IsCharacterAsset() As Boolean
        Get
            Return bIsCharAsset
        End Get
        Set(ByVal value As Boolean)
            bIsCharAsset = value
        End Set
    End Property

End Class
