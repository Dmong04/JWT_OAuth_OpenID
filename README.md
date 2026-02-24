# JWT PLECSYS - OAuth2.0
Para su correcto funcionamiento, se deben instalar las siguientes librerías en los paquetes NuGet:
<img width="950" height="455" alt="image" src="https://github.com/user-attachments/assets/e54fd22b-2516-4ffb-8d48-d6ea07dc6736" />
Estas librerías proporcionan acceso a validaciones de datos de usuario con OAuth2.0, de ahí, lo siguiente será realizar las migraciones a una base de datos (preferentemente de prueba para su prueba), obviando las tablas ya existentes para evitar conflictos.
Únicamente se necesitan las tablas generadas por OpenIdDict para almacenar los tokens de acceso y la información referente al usuario corrspondiente del Token, como se observa en las siguientes imágenes.
<img width="1109" height="661" alt="image" src="https://github.com/user-attachments/assets/19aeb7cc-37a6-4d7d-bca0-82a6781fe341" />
<img width="1091" height="628" alt="image" src="https://github.com/user-attachments/assets/9a954cc1-9c94-4364-a898-a7577eadc696" />
Los comandos de Creación y aplicación para la migración a bases de datos son los correspondientes:
dotnet ef migrations add AddOpenIddict --project INFRAESTRUCTURE --startup-project API
dotnet ef database update --project INFRAESTRUCTURE --startup-project API
Referente a la migración, esta se realizará a través de la consola del editor de código (Visual Studio) y se generarán los siguiente archivos:
<img width="367" height="88" alt="image" src="https://github.com/user-attachments/assets/e08bb48f-e782-4d20-ac30-6372c0cde1e3" />
Lo siguiente será comentar el código de creación de las tablas que ya contengamos en la base de datos (en caso de ya existir), conforme se muestra en la imagen:
<img width="1275" height="770" alt="image" src="https://github.com/user-attachments/assets/d2eb982c-22b2-4fed-b7db-1ea5ed8f11f0" />
Una vez hecho esto, podremos probar nuestros endpoints, el de generación del token de seguridad se establecerá en la clase Program.cs, de este modo:
<img width="1240" height="549" alt="image" src="https://github.com/user-attachments/assets/fde2dfab-df0d-4340-b1d0-bba4aba13fbb" />
Teniendo parte de la URL para el acceso al endpoint, podemos probarlo vía postman, resultando en la siguiente respuesta:
<img width="1386" height="876" alt="image" src="https://github.com/user-attachments/assets/1c349b9d-cfbb-4d85-b36a-95bb8ffad922" />
# NOTA IMPORTANTE
Recuerda poblar tu base de datos demo antes de realizar pruebas o inserts de usuarios, se diseñó la demo de este modo por fines prácticos.
