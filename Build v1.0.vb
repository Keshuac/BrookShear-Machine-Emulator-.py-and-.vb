Module Module1
    Sub main()
        Dim LineCount As Integer = 0
        Dim PC As Integer = 0
        Dim Register(16) As Integer
        Dim A, B, C, D, CD, temp As Integer
        Dim Tempstring As String
		Dim Location As String = "" 'Add Location here

        FileOpen(1, Location, OpenMode.Input)
        While Not EOF(1)
            LineInput(1)
            LineCount += 1
        End While

        FileClose() 'Retrieving Lincount for Memory size allocation
        Dim MemorySize As Integer = LineCount * 2 + 10
        Dim Memory(MemorySize) As String
        LineCount = 0

        FileOpen(1, Location, OpenMode.Input)
        While Not EOF(1)
            TempString = LineInput(1)
            Memory(LineCount) = TempString.Substring(0, 2)
            Memory(LineCount + 1) = TempString.Substring(2, 2)
            LineCount += 2
        End While
        FileClose(1) 'Retrieving Instructions and storing into memory

        For i = 0 To UBound(Register)
            Register(i) = &H0
        Next
        While (PC < UBound(Memory) * 2)
            A = Convert.ToInt32(Memory(PC).Substring(0, 1), 16)
            B = Convert.ToInt32(Memory(PC).Substring(1, 1), 16)
            C = Convert.ToInt32(Memory(PC + 1).Substring(0, 1), 16)
            D = Convert.ToInt32(Memory(PC + 1).Substring(1, 1), 16)
            CD = C * 10 + D

            HexDisplay(PC, Memory(PC), Memory(PC + 1), Register)

            Select Case A
                Case 0
                    Console.WriteLine("Error Code 0x00: Invalid Command: 0")
                    PC = UBound(Memory) * 3
                Case 1
                    Register(B) = Memory(CD)
                Case 2
                    Register(B) = CD
                Case 3
                    Memory(CD) = Register(B)
                Case 4
                    Register(D) = Register(C)
                Case 5
                    Register(D) = Register(B) + Register(C)
                Case 6
                    Console.WriteLine("Error Code 0x06: Invalid Command: 6")
                    PC = UBound(Memory) * 3
                Case 7
                    Register(D) = Register(B) Or Register(C)
                Case 8
                    Register(D) = Register(B) And Register(C)
                Case 9
                    Register(D) = Register(B) Xor Register(C)
                Case 10

                    For i = 0 To D
                        temp = Register(16)
                        For j = 0 To 16
                            Register(j + 1) = Register(j)
                        Next
                        Register(0) = temp
                    Next
                Case 11
                    PC = CD - 2
                Case 12
                    PC = UBound(Memory) * 3
                Case 13
                    Console.WriteLine("Error Code 0x0D: Invalid Command: D")
                    PC = UBound(Memory) * 3
                Case 14
                    Console.WriteLine("Error Code 0x0E: Invalid Command: E")
                    PC = UBound(Memory) * 3
                Case 15
                    Console.WriteLine("Error Code 0x0F: Invalid Command: F")
                    PC = UBound(Memory) * 3

            End Select
            PC += 2
        End While

    End Sub
    Sub HexDisplay(ByVal PC As Integer, ByVal Memory1 As String, ByVal Memory2 As String, ByVal Register As Integer())
        If (PC < 10) Then
            Console.Write("0" & CStr(PC) & " ")
        Else
            Console.Write(CStr(PC) & " ")
        End If
        Console.Write(CStr(Memory1) & CStr(Memory2) & " ")
        For i = 0 To UBound(Register)
            If (Register(i) < 10) Then
                Console.Write("0" & CStr(Register(i)) & " ")
            Else
                Console.Write(CStr(Register(i)) & " ")
            End If
        Next
        Console.Write(vbCrLf)
    End Sub
End Module