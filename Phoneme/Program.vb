Imports System

Module Program
    Sub Main(args As String())
        '**** VARIABLE TO TEST
        Dim vmemTexPalabraPrueba As String = "DIGITAL SOLUTIONS"
        Dim vmemDiccionarioFonemas As Dictionary(Of String, ArrayList) = diccionarioObtenerFonemasComoDiccionario(vmemTexPalabraPrueba)
        Console.WriteLine("Fonemas Vocàlicos : " & String.Join(", ", vmemDiccionarioFonemas("FonemasVocalicos").ToArray()))
        Console.WriteLine("Fonemas consonànticos : " & String.Join(", ", vmemDiccionarioFonemas("FonemasConsonanticos").ToArray()))
    End Sub

    Function diccionarioObtenerFonemasComoDiccionario(vparTexPalabraPrueba As String) As Dictionary(Of String, ArrayList)
        Dim vlocDiccionarioFonemasConsonanticos As New Dictionary(Of Char, String) From {
            {"b"c, "/B/"}, {"c"c, "/C/"}, {"d"c, "/D/"}, {"f"c, "/F/"}, {"g"c, "/G/"}, {"h"c, "/H/"}, {"j"c, "/J/"}, {"k"c, "/K/"}, {"l"c, "/L/"}, {"m"c, "/M/"},
            {"n"c, "/N/"}, {"ñ"c, "/Ñ/"}, {"p"c, "/P/"}, {"q"c, "/Q/"}, {"r"c, "/R/"}, {"s"c, "/S/"}, {"t"c, "/T/"}, {"v"c, "/V/"}, {"w"c, "/W/"}, {"x"c, "/X/"}, {"y"c, "/Y/"}, {"z"c, "/Z/"}
        }

        Dim vlocDiccionarioFonemasVocalicos As New Dictionary(Of Char, String) From {
            {"a"c, "/A/"}, {"e"c, "/E/"}, {"i"c, "/I/"}, {"o"c, "/O/"}, {"u"c, "/U/"}
        }

        vparTexPalabraPrueba = vparTexPalabraPrueba.ToLower()

        Dim vlocArrFonemasVocalicos As New ArrayList()
        Dim vlocArrFonemasConsonanticos As New ArrayList()

        For Each c As Char In vparTexPalabraPrueba
            If vlocDiccionarioFonemasConsonanticos.ContainsKey(c) Then
                vlocArrFonemasConsonanticos.Add(vlocDiccionarioFonemasConsonanticos(c))
            ElseIf vlocDiccionarioFonemasVocalicos.ContainsKey(c) Then
                vlocArrFonemasVocalicos.Add(vlocDiccionarioFonemasVocalicos(c))
            End If
        Next

        Dim vlocDiccionarioResultadoFonemas As New Dictionary(Of String, ArrayList) From {
            {"FonemasVocalicos", vlocArrFonemasVocalicos},
            {"FonemasConsonanticos", vlocArrFonemasConsonanticos}
        }

        Return vlocDiccionarioResultadoFonemas
    End Function
End Module

