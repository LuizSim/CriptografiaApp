; Informações gerais sobre o instalador
[Setup]
AppName=CriptografiAPP
AppVersion=1.0
DefaultDirName={sd}\CriptografiAPP
DefaultGroupName=CriptografiAPP
UninstallDisplayIcon={app}\CriptografiAPP.exe
OutputDir=.
OutputBaseFilename=CriptografiAPP_Installer
Compression=lzma2
SolidCompression=yes
LicenseFile=license.txt
ArchitecturesInstallIn64BitMode=x64

; Definição dos arquivos a serem incluídos
[Files]
Source: "C:\Users\luiza\OneDrive\Área de Trabalho\Projetos\CriptografiaApp\bin\Release\net7.0-windows\win-x64\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

; Criando atalhos no menu iniciar
[Icons]
Name: "{group}\CriptografiAPP"; Filename: "{app}\CriptografiAPP.exe"; WorkingDir: "{app}"; IconFilename: "{app}\CriptografiAPP.exe"

; Desinstalação do aplicativo
[UninstallDelete]
Type: filesandordirs; Name: "{app}"

; Mensagens customizadas
[Languages]
Name: "portuguese"; MessagesFile: "compiler:Languages\Portuguese.isl"

; Código adicional para mostrar informações ao usuário
[Code]
procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then
  begin
    MsgBox('A instalação foi concluída com sucesso!' + #13#10 +
           'Para abrir o CriptografiAPP, siga os seguintes passos:' + #13#10#13#10 +
           '1. Abra a pasta do seu disco local de origem.' + #13#10 +
           '2. Procure a para "Program Files".' + #13#10 +
           '3. Localize a pasta "CriptografiAPP".' + #13#10 +
           '4. Localize o arquivo "CriptografiaAPP.EXE".' + #13#10 +
           '5. Execute o programa.' + #13#10#13#10 +
           'Obrigado por instalar o CriptografiAPP!', mbInformation, MB_OK);
  end;
end;
