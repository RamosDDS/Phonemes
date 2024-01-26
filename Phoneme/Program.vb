Imports System.IO

Module Module1
    Sub Main()
        Dim vmemFrase As String = "ca"
        Dim vmemDiccionarioFonemas As Dictionary(Of String, String) = AnalizarFrase(vmemFrase)
    End Sub

    Function AnalizarFrase(frase As String) As Dictionary(Of String, String)
        Dim vlocDiccionarioFonemas As New Dictionary(Of String, String)
        Dim triptongos As Dictionary(Of String, String) = ObtenerDiccionarioDesdeArchivo("../../../resourses/tripthongDictionary.txt")
        Dim diptongos As Dictionary(Of String, String) = ObtenerDiccionarioDesdeArchivo("../../../resourses/diphthongDictionary.txt")

        Dim vlocArrTriptongos As New List(Of String)()
        Dim vlocArrDiptongos As New List(Of String)()
        Dim vlocArrConsonanticos As New List(Of String)()


        Dim palabras As String() = frase.Split(" "c)

        For Each palabra In palabras

            If diptongos.ContainsKey(palabra) Then
                Console.WriteLine(diptongos(palabra))
            End If
            Console.WriteLine(palabra)
        Next


        Return vlocDiccionarioFonemas
    End Function

    Function ObtenerDiccionarioDesdeArchivo(rutaArchivo As String) As Dictionary(Of String, String)
        ' Verificar si el archivo existe
        If File.Exists(rutaArchivo) Then
            ' Crear un diccionario para almacenar los pares clave-valor
            Dim diccionario As New Dictionary(Of String, String)

            ' Leer todas las líneas del archivo
            Dim lineas As String() = File.ReadAllLines(rutaArchivo)

            ' Procesar cada línea y agregar al diccionario
            For Each linea In lineas
                Dim partes As String() = linea.Split("="c)

                ' Verificar que la línea tiene el formato esperado
                If partes.Length = 2 Then
                    Dim clave As String = partes(0)
                    Dim valor As String = partes(1)

                    ' Agregar al diccionario
                    diccionario.Add(clave, valor)
                Else
                    Console.WriteLine("Formato incorrecto en la línea: " & linea)
                End If
            Next

            ' Devolver el diccionario
            Return diccionario
        Else
            Console.WriteLine("El archivo no existe en la ruta especificada.")
            ' Devolver null si el archivo no existe
            Return Nothing
        End If
    End Function


End Module


