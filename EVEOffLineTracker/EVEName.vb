Public Class EVEName

    'ITEMID	ITEMNAME

    Private lItemID As Long
    Public Property ItemID() As Long
        Get
            Return lItemID
        End Get
        Set(ByVal value As Long)
            lItemID = value
        End Set
    End Property

    Private sItemName As String
    Public Property ItemName() As String
        Get
            Return sItemName
        End Get
        Set(ByVal value As String)
            sItemName = value
        End Set
    End Property

End Class
