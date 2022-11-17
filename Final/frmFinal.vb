Imports System.IO

'El listBox con la fuente: courier new
Public Class frmFinal
    Dim nomArchivo As String
    Dim ubicacion As String

    'Rutinas
    Sub Eliminar(dato As ListBox)
        dato.Items.Remove(dato.SelectedItem.ToString())
    End Sub
    Sub Leer(archivo As String)
        Dim leerArchivo As New StreamReader(archivo)

        While Not leerArchivo.EndOfStream
            Me.lstDatos.Items.Add(leerArchivo.ReadLine())
        End While

        leerArchivo.Close()
    End Sub
    Sub Guardar(archivo As String)
        Dim grabarArchivo As New StreamWriter(archivo)

        'For Each item In Me.lstDatos.Items
        'grabarArchivo.WriteLine(item.ToString())
        'Next

        For i = 0 To Me.lstDatos.Items.Count() - 1
            grabarArchivo.WriteLine(Me.lstDatos.Items(i).ToString())
        Next
        grabarArchivo.Close()

        MsgBox("Archivo grabado correctamente", vbInformation)
        Me.lstDatos.Focus()
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        'Verifico que los textBox no estén vacios
        If Me.txtNombre.Text = "" Or Me.txtTelefono.Text = "" Or Me.txtDireccion.Text = "" Then
            MsgBox("Debe ingresar todos los datos", vbInformation, "Atención")
            Me.txtNombre.Focus()
            Exit Sub
        End If

        'Maximo de nombre, dirección y telefono
        Me.txtNombre.Text = Mid(Me.txtNombre.Text, 1, 30)
        Me.txtDireccion.Text = Mid(txtDireccion.Text, 1, 30)
        Me.txtTelefono.Text = Mid(Me.txtTelefono.Text, 1, 15)

        Me.lstDatos.Items.Add(
            Me.txtNombre.Text & Space(30 - Len(Me.txtNombre.Text)) & " " &
            Me.txtDireccion.Text & Space(30 - Len(Me.txtDireccion.Text)) & " " &
            Me.txtTelefono.Text & Space(15 - Len(Me.txtTelefono.Text))
        )

        Me.txtNombre.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtTelefono.Text = ""
        Me.txtNombre.Focus()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim respueta

        If Me.lstDatos.SelectedIndex = -1 Then
            MsgBox("Por favor seleccione un item de la lista", vbInformation, "Atención")
            Me.lstDatos.Focus()
            Exit Sub
        End If

        respueta = MsgBox("¿Está seguro que quiere eliminar el item?", vbYesNo, "Atención")

        If respueta = vbYes Then
            Me.Eliminar(Me.lstDatos)
            Me.txtNombre.Focus()
        End If
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        If Me.lstDatos.SelectedIndex = -1 Then
            MsgBox("Por favor seleccione un item de la lista", vbInformation, "Atención")
            Me.lstDatos.Focus()
            Exit Sub
        End If

        'Desde la posición 1 al 30, porque 30 es el máximo de caracteres permitidos
        Me.txtNombre.Text = Mid(Me.lstDatos.SelectedItem.ToString(), 1, 30)
        'Desde el 32, porque son 30+1(espacio en blanco) + 1(Desde donde empieza)
        Me.txtDireccion.Text = Mid(Me.lstDatos.SelectedItem.ToString(), 32, 30)
        'La misma lógica
        Me.txtTelefono.Text = Mid(Me.lstDatos.SelectedItem.ToString(), 63, 15)

        Me.Eliminar(Me.lstDatos)
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        If Me.txtBuscar.Visible Then
            Me.txtBuscar.Visible = False
            Exit Sub
        End If
        Me.txtBuscar.Visible = True
    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        Dim existe As Integer

        For i = 0 To Me.lstDatos.Items.Count() - 1
            existe = Me.lstDatos.Items(i).indexOf(Me.txtBuscar.Text)

            If existe <> -1 Then
                Me.lstDatos.SelectedIndex() = i
                Exit For
            End If
        Next
    End Sub

    Private Sub frmFinal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nomArchivo = "agenda.txt" 'Nombre físico del archivo
        ubicacion = "c:\intel\" 'Ucicacion donde sera almacenado
        Dim opc As Integer

        If File.Exists(ubicacion & nomArchivo) Then
            opc = MsgBox("Ya existe una agenda. ¿Desea leer su contenido?", vbYesNo + vbInformation)
            If opc = vbYes Then
                Me.Leer(ubicacion & nomArchivo)
                opc = 0
            Else '¿Sobreescribo la agenda o lo agrego los nuevos datos
                opc = 1
            End If
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Me.Guardar(ubicacion & nomArchivo)
        Me.lstDatos.Text = ""
    End Sub

    Private Sub btnLeer_Click(sender As Object, e As EventArgs) Handles btnLeer.Click
        Me.Leer(ubicacion & nomArchivo)
    End Sub

End Class
