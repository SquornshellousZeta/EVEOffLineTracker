Public Class EVEFlag

    'FLAGID	FLAGNAME	FLAGTEXT	ORDERID

    Private iFlagID As Integer
    Public Property FlagID() As Integer
        Get
            Return iFlagID
        End Get
        Set(ByVal value As Integer)
            iFlagID = value
        End Set
    End Property

    Private sFlagName As String
    Public Property FlagName() As String
        Get
            Return sFlagName
        End Get
        Set(ByVal value As String)
            sFlagName = value
        End Set
    End Property

    Private sFlagText As String
    Public Property FlagText() As String
        Get
            Return sFlagText
        End Get
        Set(ByVal value As String)
            sFlagText = value
        End Set
    End Property

End Class
