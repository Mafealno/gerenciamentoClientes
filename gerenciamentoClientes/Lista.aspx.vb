Imports System.Data.SqlClient

Public Class Lista
    Inherits System.Web.UI.Page

    Public Property htmlListaClientes As String = ""
    Public strCon = ConfigurationManager.ConnectionStrings("DefautConnetion").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim con As SqlConnection
        Dim sc As SqlCommand
        Dim sdr As SqlDataReader
        Dim query As String

        con = New SqlConnection(strCon)
        query = "SELECT *, T_CLI.ID AS CLIENTE_ID, T_CLI.TIPO AS TIPO_CLIENTE, " +
                "T_TEL.TIPO AS TIPO_TELEFONE, T_TEL.NUMERO AS NUMERO_TELEFONE, " +
                "T_END.NUMERO AS NUMERO_ENDERECO FROM T_CLIENTE T_CLI LEFT JOIN " +
                "T_TELEFONE T_TEL ON T_CLI.ID = T_TEL.ID_CLIENTE " +
                "LEFT JOIN T_ENDERECO T_END ON T_CLI.ID = T_END.ID_CLIENTE"
        con.Open()
        sc = New SqlCommand(query, con)
        sdr = sc.ExecuteReader()

        While sdr.Read()
            htmlListaClientes += "<div class='item-lista'>"
            htmlListaClientes += "<div Class='col-item-id'>" + sdr("CLIENTE_ID").ToString + "</div>"
            htmlListaClientes += "<div Class='col-item-lista'>" + sdr("NOME").ToString + "</div>"
            htmlListaClientes += "<div Class='col-item-lista'>" + sdr("SOBRENOME").ToString + "</div>"
            htmlListaClientes += "<div Class='col-item-lista'>" + sdr("RG").ToString + "</div>"
            htmlListaClientes += "<div Class='col-item-lista'>" + sdr("CPF").ToString + "</div>"
            htmlListaClientes += "<div Class='col-item-lista'>" + sdr("CNPJ").ToString + "</div>"
            htmlListaClientes += "<div Class='col-item-lista'> (" + sdr("DDD").ToString + ") " + sdr("NUMERO_TELEFONE").ToString + "</div>"
            htmlListaClientes += "</div>"
        End While
    End Sub

End Class