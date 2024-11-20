Imports System.DirectoryServices
Public Class Login

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If AutenticatheUser(TextBox1.Text, TextBox2.Text) Then
            MessageBox.Show("Usuario autenticado correctamente", "Log in", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TextBox1.Text = ""
            TextBox2.Text = ""
        Else
            MessageBox.Show("No se pudo autenticar la informacion del usuario", "Error Log On", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Dim name As String
    Dim NT As String
    Dim UserSite As String
    Private Function AutenticatheUser(Username As String, password As String) As Boolean

        Dim ret As Boolean = False
        Dim de As DirectoryEntry
        Dim dsSearch As DirectorySearcher
        Dim result As SearchResult
        Try
            de = New DirectoryEntry(GetCurrentDomain(), Username, password)
            dsSearch = New DirectorySearcher(de)
            dsSearch.Filter = "sAMAccountName=" & Username
            result = dsSearch.FindOne()
            'obtenemos el nombre del usuario
            name = result.GetDirectoryEntry().Properties("DisplayName").Value.ToString()
            'obtenemos el usuario 
            NT = result.GetDirectoryEntry().Properties("sAMAccountName").Value.ToString()
            'obtenemos el sitio al que pertenece el usuario
            UserSite = result.GetDirectoryEntry().Properties("department").Value.ToString()
            ret = True
        Catch ex As Exception
            ret = False
            MessageBox.Show(ex.Message)
        End Try
        Return ret
    End Function

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            If AutenticatheUser(TextBox1.Text, TextBox2.Text) Then
                MessageBox.Show("Usuario autenticado correctamente", "Log in", MessageBoxButtons.OK, MessageBoxIcon.Information)
                TextBox1.Text = ""
                TextBox2.Text = ""
            Else
                MessageBox.Show("No se pudo autenticar la informacion del usuario", "Error Log On", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

    End Sub
    Private Function GetCurrentDomain() As String
        Dim de As DirectoryEntry
        de = New DirectoryEntry("LDAP://RootDSE")
        Return "LDAP://" & de.Properties("defaultNamingContext")(0).ToString()
    End Function
End Class
