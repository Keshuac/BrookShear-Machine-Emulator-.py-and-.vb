def file_len(fname):
    with open(fname) as f:
        for i, l in enumerate(f):
            pass
    return i + 1   
def PrintFunction():
    if PC<10:
        print(end="0")
        print(str(PC),end=" ")
    else:
        print(PC,end=" ")
    print(Memory[PC][0:2],end="")
    print(Memory[PC+1][0:2],end=" ")
    for i in range(0,15):
        print(Register[i][2:].zfill(2), end=" ")
    print(Register[16][2:].zfill(2))
def main():
    global Memory
    global PC
    global Register
    LenFile = file_len("Project.txt")
    Memory = []

    f = open("Project.txt", "r")                        # Reading File
    ContentRead = f.readlines()
    f.close
    
    for i in range(0,LenFile):
        Memory.append(ContentRead[i][0:2])
        Memory.append(ContentRead[i][2:4])

    PC = 0
    Register = ['0x0']
    for i in range(0,16):                               # Initialiting List to 20 Register storage
        Register.append(hex(0))
    
    while PC <LenFile*2:
        Hex1 = int(Memory[PC][0:1],16)
        Hex2 = int(Memory[PC][1:2],16)
        Hex3 = int(Memory[PC+1][0:1],16)
        Hex4 = int(Memory[PC+1][1:2],16)
        Hex3_4 =int(Memory[PC+1][0:2],16)        
        PrintFunction()
        if Hex1 == 0:
            print("Exception 0x00: Command is invalid (0)")
        if Hex1 ==1:
            Register[Hex2] = Register[Hex3_4]
        if Hex1 ==2:
            Register[Hex2] = hex(Hex3_4)
        if Hex1 ==3:          #Does this delete the Register at location r?
            Memory[Hex2] = Register[Hex3_4]
        if Hex1 ==4:
            Register[Hex4]=hex(Hex3)
        if Hex1 ==5:
            Register[Hex4]= hex(int(Register[Hex3],16) + int(Register[Hex2],16))
        if Hex1 ==6:
            print("Exception 0x06: Command is invalid (6)")
        if Hex1 ==7:
            Register[Hex4] = hex(Hex3) | hex(Hex2)
        if Hex1 ==8:
            Register[Hex4] = hex(Hex3) & hex(Hex2)
        if Hex1 ==9:
            Register[Hex4] = hex(Hex3) ^ hex(Hex2)
        if Hex1 ==10:
            for i in range(0,Hex4):
                temp = Register[16]
                for i in range(0,16):
                    Register[i+1]=Register[i]
                Register[0]= temp
        if Hex1 ==11:
            PC = Hex3_4 -2
        if Hex1 ==12:
            return()
        if Hex1 ==13:
            print("Exception 0x0D: Command is invalid (D)")
        if Hex1 ==14:
            print("Exception 0x0E: Command is invalid (E)")
        if Hex1 ==15:
            print("Exception 0x0F: Command is invalid (F)")

        PC= PC+2
    input()
main()