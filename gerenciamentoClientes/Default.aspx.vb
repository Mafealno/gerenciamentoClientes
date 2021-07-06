Imports System.Data.SqlClient
Imports System.Net.Http
Imports Newtonsoft.Json

Public Class Defaut
    Inherits System.Web.UI.Page
    Public Property htmlRenderizar As String = ""
    Public Property htmlMostrarIni As String = ""
    Public Property htmlMostrarFim As String = ""
    Public strCon = ConfigurationManager.ConnectionStrings("DefautConnetion").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RenderizarTipoCliente()
        RenderizarBtnLimpar(False)
    End Sub

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoCliente.SelectedIndexChanged
        RenderizarTipoCliente()
    End Sub

    Protected Sub BtnBuscarCep_Click(sender As Object, e As EventArgs) Handles btnBuscarCep.Click
        GetEndereco()
    End Sub
    Protected Sub BtnBuscarCliente_Click(sender As Object, e As EventArgs) Handles btnBuscarCliente.Click
        GetClientesById()
        RenderizarBtnLimpar(True)
    End Sub
    Protected Sub BtnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        If ValidarDados("") = True Then
            If InsertCliente() = 1 Then
                InsertEndereco()
                InserTelefone()
                txtIdCliente.Text = Dlookup("ID", "T_CLIENTE", "")
                RenderizarBtnLimpar(True)
            End If
        End If
    End Sub
    Protected Sub BtnAtualizar_Click(sender As Object, e As EventArgs) Handles btnAtualizar.Click
        If ValidarDados("update") = True Then
            If UpdateCliente() = 1 Then
                UpdateTelefone()
                UpdateEndereco()
                RenderizarBtnLimpar(True)
            End If
        End If
    End Sub
    Protected Sub BtnDeletar_Click(sender As Object, e As EventArgs) Handles btnDeletar.Click
        If txtIdCliente.Text <> "" Then
            DeleteTelefone()
            DeleteEndereco()
            DeleteCliente()
            LimparCampos()
        End If
    End Sub
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        LimparCampos()
    End Sub
    Private Async Sub GetEndereco()
        Using client = New HttpClient
            Using response = Await client.GetAsync("http://viacep.com.br/ws/" + txtCepEndereco.Text + "/json/")
                If response.IsSuccessStatusCode Then
                    Dim StrJson = Await response.Content.ReadAsStringAsync
                    Dim endereco = JsonConvert.DeserializeObject(StrJson)

                    txtLogradouroEndereco.Text = endereco("logradouro")
                    txtComplementoEndereco.Text = endereco("complemento")
                    txtBairroEndereco.Text = endereco("bairro")
                    txtCidadeEndereco.Text = endereco("localidade")
                    txtUfEndereco.Text = endereco("uf")
                End If
            End Using
        End Using
    End Sub
    Private Sub GetClientesById()
        Dim con As SqlConnection
        Dim sc As SqlCommand
        Dim sdr As SqlDataReader
        Dim query As String

        con = New SqlConnection(strCon)
        query = "SELECT *, T_CLI.ID AS CLIENTE_ID, T_CLI.TIPO AS TIPO_CLIENTE, " +
                "T_TEL.TIPO AS TIPO_TELEFONE, T_TEL.NUMERO AS NUMERO_TELEFONE, " +
                "T_END.NUMERO AS NUMERO_ENDERECO FROM T_CLIENTE T_CLI LEFT JOIN " +
                "T_TELEFONE T_TEL ON T_CLI.ID = T_TEL.ID_CLIENTE " +
                "LEFT JOIN T_ENDERECO T_END ON T_CLI.ID = T_END.ID_CLIENTE " +
                "WHERE T_CLI.ID = " + txtIdCliente.Text
        con.Open()
        sc = New SqlCommand(query, con)
        sdr = sc.ExecuteReader()

        While sdr.Read()
            txtIdCliente.Text = sdr("CLIENTE_ID").ToString()
            txtNomeCliente.Text = sdr("NOME").ToString()
            txtSobrenomeCliente.Text = sdr("SOBRENOME").ToString()
            ddlTipoCliente.SelectedValue = sdr("TIPO_CLIENTE").ToString()
            txtRgCliente.Text = sdr("RG").ToString()
            txtCpfCliente.Text = sdr("CPF").ToString()
            txtCnpjCliente.Text = sdr("CNPJ").ToString()
            ddlTipoTelefone.SelectedValue = sdr("TIPO_TELEFONE").ToString()
            txtDddTelefone.Text = sdr("DDD").ToString()
            txtNumeroTelefone.Text = sdr("NUMERO_TELEFONE").ToString()
            txtCepEndereco.Text = sdr("CEP").ToString()
            txtLogradouroEndereco.Text = sdr("LOGRADOURO").ToString()
            txtNumeroEndereco.Text = sdr("NUMERO_ENDERECO").ToString()
            txtComplementoEndereco.Text = sdr("COMPLEMENTO").ToString()
            txtBairroEndereco.Text = sdr("BAIRRO").ToString()
            txtCidadeEndereco.Text = sdr("CIDADE").ToString()
            txtUfEndereco.Text = sdr("UF").ToString()
        End While
        con.Close()
    End Sub
    Private Function InsertCliente()
        Dim con As SqlConnection
        Dim sc As SqlCommand

        Dim status As Integer
        Dim query As String

        Try
            query = "INSERT INTO T_CLIENTE VALUES " +
                    "('" + txtNomeCliente.Text + "', " +
                    "'" + txtSobrenomeCliente.Text + "', " +
                    "'" + txtRgCliente.Text + "', " +
                    "'" + txtCpfCliente.Text + "', " +
                    "'" + txtCnpjCliente.Text + "', " +
                    "'" + ddlTipoCliente.SelectedItem.Value + "')"
            con = New SqlConnection(strCon)
            con.Open()
            sc = New SqlCommand(query, con)
            status = sc.ExecuteNonQuery()
            con.Close()

        Catch ex As Exception
        End Try
        Return status
    End Function
    Private Sub InserTelefone()
        Dim con As SqlConnection
        Dim sc As SqlCommand

        Dim status As Integer
        Dim query As String
        Dim idCliente As String

        idCliente = Dlookup("ID", "T_CLIENTE", "")

        Try
            con = New SqlConnection(strCon)
            query = "INSERT INTO T_TELEFONE VALUES " +
                    "('" + txtDddTelefone.Text + "', " +
                    "'" + txtNumeroTelefone.Text + "', " +
                    "'" + ddlTipoTelefone.SelectedItem.Value + "', " +
                    "'" + idCliente + "')"
            con.Open()
            sc = New SqlCommand(query, con)
            status = sc.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub InsertEndereco()
        Dim con As SqlConnection
        Dim sc As SqlCommand

        Dim status As Integer
        Dim query As String
        Dim idCliente As String

        idCliente = Dlookup("ID", "T_CLIENTE", "")

        Try
            con = New SqlConnection(strCon)
            query = "INSERT INTO T_ENDERECO VALUES " +
                    "('" + txtCepEndereco.Text + "', " +
                    "'" + txtLogradouroEndereco.Text + "', " +
                    "'" + txtNumeroEndereco.Text + "', " +
                    "'" + txtComplementoEndereco.Text + "', " +
                    "'" + txtBairroEndereco.Text + "', " +
                    "'" + txtCidadeEndereco.Text + "', " +
                    "'" + txtUfEndereco.Text + "', " +
                    "'" + idCliente + "')"
            con.Open()
            sc = New SqlCommand(query, con)
            status = sc.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
        End Try
    End Sub
    Private Function UpdateCliente()
        Dim con As SqlConnection
        Dim sc As SqlCommand

        Dim status As Integer
        Dim query As String

        Try
            con = New SqlConnection(strCon)
            query = "UPDATE T_CLIENTE SET " +
                    "NOME = '" + txtNomeCliente.Text + "', " +
                    "SOBRENOME = '" + txtSobrenomeCliente.Text + "', " +
                    "RG = '" + txtRgCliente.Text + "', " +
                    "CPF = '" + txtCpfCliente.Text + "', " +
                    "CNPJ = '" + txtCnpjCliente.Text + "', " +
                    "TIPO = '" + ddlTipoCliente.SelectedItem.Value + "' " +
                    "WHERE ID = " + txtIdCliente.Text
            con.Open()
            sc = New SqlCommand(query, con)
            status = sc.ExecuteNonQuery()
            con.Close()

        Catch ex As Exception
        End Try

        Return status
    End Function
    Private Sub UpdateTelefone()
        Dim con As SqlConnection
        Dim sc As SqlCommand

        Dim query As String

        Try
            query = "UPDATE T_TELEFONE SET " +
                "DDD = '" + txtDddTelefone.Text + "', " +
                "NUMERO = '" + txtNumeroTelefone.Text + "', " +
                "TIPO = '" + ddlTipoTelefone.SelectedItem.Value + "' " +
                "WHERE ID_CLIENTE = " + txtIdCliente.Text
            con = New SqlConnection(strCon)
            con.Open()
            sc = New SqlCommand(query, con)
            sc.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
        End Try

    End Sub
    Private Sub UpdateEndereco()
        Dim con As SqlConnection
        Dim sc As SqlCommand

        Dim query As String

        Try
            query = "UPDATE T_ENDERECO SET " +
                    "CEP = '" + txtCepEndereco.Text + "', " +
                    "LOGRADOURO = '" + txtLogradouroEndereco.Text + "', " +
                    "NUMERO = '" + txtNumeroEndereco.Text + "', " +
                    "COMPLEMENTO = '" + txtComplementoEndereco.Text + "', " +
                    "BAIRRO = '" + txtBairroEndereco.Text + "', " +
                    "CIDADE = '" + txtCidadeEndereco.Text + "', " +
                    "UF = '" + txtUfEndereco.Text + "' " +
                    "WHERE ID_CLIENTE = " + txtIdCliente.Text
            con = New SqlConnection(strCon)
            con.Open()
            sc = New SqlCommand(query, con)
            sc.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DeleteCliente()
        Dim con As SqlConnection
        Dim sc As SqlCommand

        Dim query As String

        Try
            con = New SqlConnection(strCon)
            query = "DELETE FROM T_CLIENTE WHERE ID = " + txtIdCliente.Text
            con.Open()
            sc = New SqlCommand(query, con)
            sc.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DeleteTelefone()
        Dim con As SqlConnection
        Dim sc As SqlCommand

        Dim query As String

        Try
            con = New SqlConnection(strCon)
            query = "DELETE FROM T_TELEFONE WHERE ID_CLIENTE = " + txtIdCliente.Text
            con.Open()
            sc = New SqlCommand(query, con)
            sc.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DeleteEndereco()
        Dim con As SqlConnection
        Dim sc As SqlCommand

        Dim query As String

        Try
            con = New SqlConnection(strCon)
            query = "DELETE FROM T_ENDERECO WHERE ID_CLIENTE = " + txtIdCliente.Text
            con.Open()
            sc = New SqlCommand(query, con)
            sc.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Function ValidarDados(tipoAcao As String)
        Dim valido As Boolean = True

        valido = IIf(txtNomeCliente.Text <> "", valido, False)
        valido = IIf(txtSobrenomeCliente.Text <> "", valido, False)
        If (ddlTipoCliente.SelectedItem.Value = "F") Then
            valido = IIf(txtRgCliente.Text <> "", valido, False)
            valido = IIf(txtCpfCliente.Text <> "", valido, False)
        Else
            valido = IIf(txtCnpjCliente.Text <> "", valido, False)
        End If
        valido = IIf(ddlTipoTelefone.SelectedItem.Value <> "N", valido, False)
        valido = IIf(txtDddTelefone.Text <> "", valido, False)
        valido = IIf(txtNumeroTelefone.Text <> "", valido, False)
        valido = IIf(txtCepEndereco.Text <> "", valido, False)
        valido = IIf(txtLogradouroEndereco.Text <> "", valido, False)
        valido = IIf(txtBairroEndereco.Text <> "", valido, False)
        valido = IIf(txtCidadeEndereco.Text <> "", valido, False)
        valido = IIf(txtUfEndereco.Text <> "", valido, False)

        If tipoAcao = "update" Then
            valido = IIf(txtIdCliente.Text <> "", valido, False)
        End If

        Return valido
    End Function
    Private Function Dlookup(campoBuscado As String, tabela As String, where As String)
        Dim con As SqlConnection
        Dim sc As SqlCommand
        Dim sdr As SqlDataReader

        Dim query As String
        Dim valorBuscado As String = ""

        Try
            con = New SqlConnection(strCon)
            If where = "" Then
                query = "SELECT TOP 1 " + campoBuscado + " FROM " + tabela + " ORDER BY " + campoBuscado + " DESC"
            Else
                query = "SELECT TOP 1 " + campoBuscado + " FROM " + tabela + " WHERE " + where
            End If
            con.Open()
            sc = New SqlCommand(query, con)
            sdr = sc.ExecuteReader()
            While sdr.Read()
                valorBuscado = sdr(campoBuscado).ToString()
            End While
            con.Close()
        Catch ex As Exception

        End Try

        Return valorBuscado
    End Function
    Private Sub LimparCampos()
        txtIdCliente.Text = ""
        txtNomeCliente.Text = ""
        txtSobrenomeCliente.Text = ""
        ddlTipoCliente.SelectedValue = "F"
        txtRgCliente.Text = ""
        txtCpfCliente.Text = ""
        txtCnpjCliente.Text = ""
        ddlTipoTelefone.SelectedValue = "N"
        txtDddTelefone.Text = ""
        txtNumeroTelefone.Text = ""
        txtCepEndereco.Text = ""
        txtLogradouroEndereco.Text = ""
        txtNumeroEndereco.Text = ""
        txtComplementoEndereco.Text = ""
        txtBairroEndereco.Text = ""
        txtCidadeEndereco.Text = ""
        txtUfEndereco.Text = ""

        RenderizarBtnLimpar(False)
    End Sub

    Private Sub RenderizarTipoCliente()
        Select Case ddlTipoCliente.SelectedItem.Value
            Case "F"
                lblRgCliente.Text = "RG *"
                lblCpfCliente.Text = "CPF *"
                lblCnpjCliente.Text = "CNPJ"
            Case "J"
                lblRgCliente.Text = "RG"
                lblCpfCliente.Text = "CPF"
                lblCnpjCliente.Text = "CNPJ *"
        End Select
    End Sub
    Private Sub RenderizarBtnLimpar(mostrar As Boolean)
        If mostrar = True Then
            htmlMostrarIni = "<div class='d-block'>"
            htmlMostrarFim = "</div>"
        Else
            htmlMostrarIni = "<div class='d-none'>"
            htmlMostrarFim = "</div>"
        End If
    End Sub
End Class