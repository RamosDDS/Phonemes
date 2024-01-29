Imports System.IO

Module Module1
    Sub Main()
        Dim vmemFrase As String = "casa aeropuerto esternocleidomastoideo"
        Dim vmemArrFonemas As ArrayList = ArrObtenerFonemasEnArray(vmemFrase)
        Dim vmemresult As String = String.Join(", ", vmemArrFonemas.ToArray())
        Console.WriteLine("Fonemas Encontrados ::: [ {0} ]", vmemresult)
    End Sub

    Function ArrObtenerFonemasEnArray(frase As String) As ArrayList

        Dim vlocDiccionarioFonemasTriptongos As Dictionary(Of String, String) = ObtenerDiccionarioDesdeArchivo("../../../resourses/tripthongDictionary.txt")
        Dim vlocDiccionarioFonemasDiptongos As Dictionary(Of String, String) = ObtenerDiccionarioDesdeArchivo("../../../resourses/diphthongDictionary.txt")
        Dim vlocDiccionarioFonemasConsonanticos As Dictionary(Of String, String) = ObtenerDiccionarioDesdeArchivo("../../../resourses/consonantDictionary.txt")
        Dim vlocDiccionarioFonemasCV As Dictionary(Of String, String) = ObtenerDiccionarioDesdeArchivo("../../../resourses/cvDictionary.txt")

        Dim vlocArrFonemasEncontrados As ArrayList = New ArrayList()

        Dim palabras As String() = frase.Split(" "c)

        For Each palabra In palabras

            Dim vlocSenTamanoPalabra As Integer = palabra.Length

            Dim i As Integer = 0

            While i < vlocSenTamanoPalabra
                Dim letraActual As Char = palabra(i)
                Dim letraSiguiente As Char
                Dim letraDespuesDeSiguiente As Char

                If i + 1 And i + 2 < vlocSenTamanoPalabra Then
                    letraSiguiente = palabra(i + 1)
                    letraDespuesDeSiguiente = palabra(i + 2)
                    'Console.WriteLine($"1:{letraActual} 2:{letraSiguiente} 3:{letraDespuesDeSiguiente}")
                    If EsVocal(letraActual) AndAlso EsVocal(letraSiguiente) AndAlso EsVocal(letraDespuesDeSiguiente) Then
                        Dim posibleTriptongo As String = letraActual & letraSiguiente & letraDespuesDeSiguiente
                        'Console.WriteLine($"Triptongo encontrado: {posibleTriptongo}")
                        If vlocDiccionarioFonemasTriptongos.ContainsKey(posibleTriptongo) Then
                            vlocArrFonemasEncontrados.Add(vlocDiccionarioFonemasTriptongos(posibleTriptongo))
                        End If
                    End If
                End If
                i += 1
            End While

            If i + 1 > vlocSenTamanoPalabra Then

                Dim contadorDiptongo As Integer = 0
                While contadorDiptongo < vlocSenTamanoPalabra
                    Dim letraActual As Char = palabra(contadorDiptongo)
                    Dim letraSiguiente As Char

                    If contadorDiptongo + 1 < vlocSenTamanoPalabra Then
                        letraSiguiente = palabra(contadorDiptongo + 1)
                        'Console.WriteLine($"1:{letraActual} 2:{letraSiguiente}")
                        If EsVocal(letraActual) AndAlso EsVocal(letraSiguiente) Then
                            Dim posibleDiptongo As String = letraActual & letraSiguiente
                            'Console.WriteLine($"Diptongo encontrado: {posibleDiptongo}")
                            If vlocDiccionarioFonemasDiptongos.ContainsKey(posibleDiptongo) Then
                                vlocArrFonemasEncontrados.Add(vlocDiccionarioFonemasDiptongos(posibleDiptongo))
                            End If
                        ElseIf Not EsVocal(letraActual) AndAlso Not EsVocal(letraSiguiente) Then
                            Dim grupoConsonantico As String = letraActual & letraSiguiente
                            'Console.WriteLine($"Grupo consonántico encontrado: {grupoConsonantico}")
                            If vlocDiccionarioFonemasConsonanticos.ContainsKey(grupoConsonantico) Then
                                vlocArrFonemasEncontrados.Add(vlocDiccionarioFonemasConsonanticos(grupoConsonantico))
                            End If
                        ElseIf Not EsVocal(letraActual) AndAlso EsVocal(letraSiguiente) Then
                            Dim grupocv As String = letraActual & letraSiguiente
                            'Console.WriteLine($"Grupo CV : {grupocv}")
                            If vlocDiccionarioFonemasCV.ContainsKey(grupocv) Then
                                vlocArrFonemasEncontrados.Add(vlocDiccionarioFonemasCV(grupocv))
                            End If
                        End If
                    End If
                    contadorDiptongo += 1
                End While


            End If

        Next

        Return vlocArrFonemasEncontrados
    End Function

    Function ObtenerDiccionarioDesdeArchivo(rutaArchivo As String) As Dictionary(Of String, String)
        If File.Exists(rutaArchivo) Then
            Dim diccionario As New Dictionary(Of String, String)
            Dim lineas As String() = File.ReadAllLines(rutaArchivo)
            For Each linea In lineas
                Dim partes As String() = linea.Split("="c)
                If partes.Length = 2 Then
                    Dim clave As String = partes(0)
                    Dim valor As String = partes(1)
                    diccionario.Add(clave, valor)
                Else
                    Console.WriteLine("Formato incorrecto en la l�nea: " & linea)
                End If
            Next
            Return diccionario
        Else
            Console.WriteLine("El archivo no existe en la ruta especificada.")
            Return Nothing
        End If
    End Function

    Function EsVocal(letra As Char) As Boolean
        Dim vocales As String = "aeiouAEIOU"
        Return vocales.Contains(letra)
    End Function


End Module


