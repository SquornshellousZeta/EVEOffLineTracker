Public Class frmLoading

    Delegate Sub SetTextInvoker(ByVal TextToDisplay As String)

    Public Sub SetText(ByVal TextToDisplay As String)
        If lblText.InvokeRequired Then
            lblText.Invoke(New SetTextInvoker(AddressOf SetText), New Object() {TextToDisplay})
        Else
            lblText.Text = TextToDisplay
        End If
    End Sub

End Class