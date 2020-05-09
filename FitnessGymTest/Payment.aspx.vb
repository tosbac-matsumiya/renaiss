Public Class Payment
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblSum.Text = CInt(Session("celsum").ToString).ToString("#,0")
    End Sub

End Class