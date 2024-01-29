Imports System.IO

Module Module1
    Sub Main()
        Dim vmemFrase As String = "casa aeropuerto esternocleidomastoideo"
        Dim vmemArrFonemas As ArrayList = ArrObtenerFonemasEnArray(vmemFrase)
        Dim vmemTexResult As String = String.Join(", ", vmemArrFonemas.ToArray())
        Console.WriteLine("Fonemas Encontrados ::: [ {0} ]", vmemTexResult)
    End Sub

    Function ArrObtenerFonemasEnArray(vparTexFrase As String) As ArrayList

        Dim vlocDiccionarioFonemasTriptongos As Dictionary(Of String, String) = obtenerDiccionarioDesdeArchivo("../../../resourses/tripthongDictionary.txt")
        Dim vlocDiccionarioFonemasDiptongos As Dictionary(Of String, String) = obtenerDiccionarioDesdeArchivo("../../../resourses/diphthongDictionary.txt")
        Dim vlocDiccionarioFonemasConsonanticos As Dictionary(Of String, String) = obtenerDiccionarioDesdeArchivo("../../../resourses/consonantDictionary.txt")
        Dim vlocDiccionarioFonemasCV As Dictionary(Of String, String) = obtenerDiccionarioDesdeArchivo("../../../resourses/cvDictionary.txt")

        Dim vlocArrFonemasEncontrados As ArrayList = New ArrayList()

        Dim vlocTexPalabras As String() = vparTexFrase.Split(" "c)

        For Each vlocTexpalabra In vlocTexPalabras

            Dim vlocSenTamanoPalabra As Integer = vlocTexpalabra.Length

            Dim vlocSenIndice As Integer = 0

            While vlocSenIndice < vlocSenTamanoPalabra
                Dim vlocTexLetraActual As Char = vlocTexpalabra(vlocSenIndice)
                Dim vlocTexLetraSiguiente As Char
                Dim vlocTexLetraDespuesDeSiguiente As Char

                If vlocSenIndice + 1 And vlocSenIndice + 2 < vlocSenTamanoPalabra Then
                    vlocTexLetraSiguiente = vlocTexpalabra(vlocSenIndice + 1)
                    vlocTexLetraDespuesDeSiguiente = vlocTexpalabra(vlocSenIndice + 2)
                    'Console.WriteLine($"1:{letraActual} 2:{letraSiguiente} 3:{letraDespuesDeSiguiente}")
                    If EsVocal(vlocTexLetraActual) AndAlso EsVocal(vlocTexLetraSiguiente) AndAlso EsVocal(vlocTexLetraDespuesDeSiguiente) Then
                        Dim vlocTexPosibleTriptongo As String = vlocTexLetraActual & vlocTexLetraSiguiente & vlocTexLetraDespuesDeSiguiente
                        'Console.WriteLine($"Triptongo encontrado: {posibleTriptongo}")
                        If vlocDiccionarioFonemasTriptongos.ContainsKey(vlocTexPosibleTriptongo) Then
                            vlocArrFonemasEncontrados.Add(vlocDiccionarioFonemasTriptongos(vlocTexPosibleTriptongo))
                        End If
                    End If
                End If
                vlocSenIndice += 1
            End While

            If vlocSenIndice + 1 > vlocSenTamanoPalabra Then

                Dim vlocSenContadorIndiceDiptongo As Integer = 0
                While vlocSenContadorIndiceDiptongo < vlocSenTamanoPalabra
                    Dim vlocTexLetraActual As Char = vlocTexpalabra(vlocSenContadorIndiceDiptongo)
                    Dim vlocTexLetraSiguiente As Char

                    If vlocSenContadorIndiceDiptongo + 1 < vlocSenTamanoPalabra Then
                        vlocTexLetraSiguiente = vlocTexpalabra(vlocSenContadorIndiceDiptongo + 1)
                        'Console.WriteLine($"1:{letraActual} 2:{letraSiguiente}")
                        If EsVocal(vlocTexLetraActual) AndAlso EsVocal(vlocTexLetraSiguiente) Then
                            Dim vlocTexposibleDiptongo As String = vlocTexLetraActual & vlocTexLetraSiguiente
                            'Console.WriteLine($"Diptongo encontrado: {posibleDiptongo}")
                            If vlocDiccionarioFonemasDiptongos.ContainsKey(vlocTexposibleDiptongo) Then
                                vlocArrFonemasEncontrados.Add(vlocDiccionarioFonemasDiptongos(vlocTexposibleDiptongo))
                            End If
                        ElseIf Not EsVocal(vlocTexLetraActual) AndAlso Not EsVocal(vlocTexLetraSiguiente) Then
                            Dim vlocTexGrupoConsonantico As String = vlocTexLetraActual & vlocTexLetraSiguiente
                            'Console.WriteLine($"Grupo consonántico encontrado: {grupoConsonantico}")
                            If vlocDiccionarioFonemasConsonanticos.ContainsKey(vlocTexGrupoConsonantico) Then
                                vlocArrFonemasEncontrados.Add(vlocDiccionarioFonemasConsonanticos(vlocTexGrupoConsonantico))
                            End If
                        ElseIf Not EsVocal(vlocTexLetraActual) AndAlso EsVocal(vlocTexLetraSiguiente) Then
                            Dim vlocTexGrupocv As String = vlocTexLetraActual & vlocTexLetraSiguiente
                            'Console.WriteLine($"Grupo CV : {grupocv}")
                            If vlocDiccionarioFonemasCV.ContainsKey(vlocTexGrupocv) Then
                                vlocArrFonemasEncontrados.Add(vlocDiccionarioFonemasCV(vlocTexGrupocv))
                            End If
                        End If
                    End If
                    vlocSenContadorIndiceDiptongo += 1
                End While


            End If

        Next

        Return vlocArrFonemasEncontrados
    End Function

    Function obtenerDiccionarioDesdeArchivo(vparTexRutaArchivo As String) As Dictionary(Of String, String)
        If File.Exists(vparTexRutaArchivo) Then
            Dim vlocDiccionario As New Dictionary(Of String, String)
            Dim vlocTexLineas As String() = File.ReadAllLines(vparTexRutaArchivo)
            For Each vlocTexLinea In vlocTexLineas
                Dim vlocTexPartesLinea As String() = vlocTexLinea.Split("="c)
                If vlocTexPartesLinea.Length = 2 Then
                    Dim vlocTexClave As String = vlocTexPartesLinea(0)
                    Dim vlocTexValor As String = vlocTexPartesLinea(1)
                    vlocDiccionario.Add(vlocTexClave, vlocTexValor)
                Else
                    Console.WriteLine("Formato incorrecto en la linea: " & vlocTexLinea)
                End If
            Next
            Return vlocDiccionario
        Else
            Console.WriteLine("El archivo no existe en la ruta especificada.")
            Return Nothing
        End If
    End Function

    Function EsVocal(vparTexLetra As Char) As Boolean
        Dim vlocTexVocales As String = "aeiouAEIOU"
        Return vlocTexVocales.Contains(vparTexLetra)
    End Function


End Module


