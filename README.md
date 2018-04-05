This project file contains a synchronous socket code for the Microsoft HoloLens. When we first came across the usage of sockets for the HoloLens, we found that the normal System.Sockets was not supported by the Microsoft HoloLens. Hence, we had to use the Windows.Networking.Sockets to create our socket, which was not supported by Unity 3D (The development platform for Microsoft HoloLens). 
Therefore, we used #if !UNITY_EDITOR tag to make sure the application compiled. 
You will see that we used StreamWriter and StreamReader class from the System.IO package to send and receive data. 

NOTE: Please note that the program is just a simple socket, without any functionality. Just add the functionality you need to the existing code inside the Update or Start function. 