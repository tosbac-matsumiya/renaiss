Imports System.Data.SqlClient

Public Class Thankyou
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.lblMemberCd.Text = Session("membercd").ToString
        Me.lblName.Text = Session("name_kanji").ToString
        Me.lblTenpo.Text = Session("facility_name").ToString
        Me.lblSyubetu.Text = Session("plan_name").ToString
        Dim day As String = Session("date_visit").ToString
        Me.lblRaiten.Text = Mid(day, 1, 4) + "年" + CStr(CInt(Mid(day, 6, 2))) + "月" + CStr(CInt(Mid(day, 9, 2))) + "日"
        Dim sum As Decimal = CDec(Session("CelSum").ToString)
        Me.lblHiyou.Text = sum.ToString("#,0") + "円"
        Me.lblGoriyokakijo.Text = Session("facility_name").ToString + "店"
        Me.lblUketukebi.Text = Today.ToString("yyyy年MM月dd日(ddd)")

        Session("name_kanji") = "" '会員名を空白 'ADD_2017/09/27 TOS163



    End Sub

End Class