# Alinhamentos de formato de código e ferramentas para CR.

Para o desenvolvimento em .net com C# entre um grupo de desenvolvedores, é importante a criação de um 
formato estandardizado e bem limpo das boas práticas do código, para manter uma consistência no trabalho 
de cada um e ajudar a quem faz o Code Review a poder ler mais fácil o conteúdo sem ter tanto ruido na 
tela das alterações.
	
Para Visual Studio, existe uma ferramenta sem necessidade de plugin para definir as regras a serem 
aplicadas: Editor Config.

## Example file

Below is an example .editorconfig file setting end-of-line and indentation styles for Python and 
JavaScript files.

```ini
# EditorConfig is awesome: https://editorconfig.org

# top-most EditorConfig file
root = true

# Unix-style newlines with a newline ending every file
[*]
end_of_line = lf
insert_final_newline = true

# Matches multiple files with brace expansion notation
# Set default charset
[*.{js,py}]
charset = utf-8

# 4 space indentation
[*.py]
indent_style = space
indent_size = 4

# Tab indentation (no size specified)
[Makefile]
indent_style = tab

# Indentation override for all JS under lib directory
[lib/**.js]
indent_style = space
indent_size = 2

# Matches the exact files either package.json or .travis.yml
[{package.json,.travis.yml}]
indent_style = space
indent_size = 2
```
> [Editor config example](https://editorconfig.org/#example-file)

O editor config funciona perfeitamente no Visual Studio sem plugin e ainda pode definir regras específicas 
usando regexp para os arquivos de interesse, permitindo dimensionar as regras para várias linguagens de 
programação, tem uma sintaxe declarativa e possui uma documentação ampla e com uma comunidade grande de 
desenvolvedores que indicam o uso.

As regras de formato são aplicadas imediatamente que definidas no arquivo .editorconfig no visual studio 
no mesmo nível da solução (arquivo .sln). o dotnet interpreter para as versões 6.0 ou maior possue uns 
comandos para aplicar ou diagnosticar a limpeza de código, por padrão, ele possui regras de Microsoft 
definidas no internal do IDE Visual Studio, quando definido o arquivo .editorconfig, ele passa a usar 
essas regras para a solução imediata definida.

O comando para aplicar as regras de limpeza de código é
```bash
dotnet format <solutionPath.sln>
```

Esse comando aplica as regras diretamente no código de todos os arquivos da solução, o direito dele assim
não é para ser usado em um ambiente de desenvolvimento, ele é mais recomendado para ser usado em um 
ambiente de integração contínua, onde o código é verificado e formatado antes de ser integrado ao branch 
principal, evitando que o código com formatação incorreta seja integrado ao projeto.

Para o desenvolvimento é importante usar um param que indica que apenas quer fazer um análise do código,
sem aplicar as regras, para isso, o comando é
```bash
dotnet format --verify-no-changes <solutionPath.sln>
```

o mesmo comando pode ter um param para especificar o arquivo ou pasta de interesse, para isso pode adicionar
o param --include ao final do comando, como no exemplo abaixo
```bash
dotnet format --verify-no-changes <solutionPath.sln> --include <folderPath>
```

isso ajuda ao desenvolvedor a verificar se o código que ele alterou ou criou está seguindo as regras de
formatação definidas, também garante que possa ver quais regras estão sendo aplicadas, pois geralmente não
é preciso aplicar todas as regras de formatação que o visual studio tem, pois algumas podem não ser de 
interesse para o time, com isso pode levantar o caso de ignorar algumas regras conversando com o time.

o editorconfig possui um suporte amplo para definir essas regras, então, facilmente pode ser pesquisada a 
regra de interesse e adicionada caso seja necessária.

Para saber se realmente está a executar as regras do editorconfig, pode ser usado os params `--verbosity diagnostic`
para ter os detalhes e verificar quais regras estão sendo aplicadas, o comando seria tal que
```bash
dotnet format --verify-no-changes <solutionPath.sln> --verbosity diagnostic --include <folderPath>
```

Um output de exemplo com o comando de acima e com o projeto atual seria algo como
```info
PS C:\Users\diego.busnego\source\repos\editorconfigExample> dotnet format --verify-no-changes .\editorconfigExample.sln --verbosity diagnostic --include .\Program.cs

  A versão do dotnet runtime é '9.0.13'.
  Usando MSBuild.exe localizado em 'C:\Program Files\dotnet\sdk\9.0.311\'.
  Formatação de arquivos de código no espaço de trabalho 'C:\Users\diego.busnego\source\repos\editorconfigExample\editorconfigExample.sln'.
  Carregando espaço de trabalho.
    Determinando os projetos a serem restaurados...
  Todos os projetos estão atualizados para restauração.
  O projeto editorconfigExample está usando a configuração de 'C:\Users\diego.busnego\source\repos\editorconfigExample\.editorconfig'.
  O projeto editorconfigExample está usando a configuração de 'C:\Users\diego.busnego\source\repos\editorconfigExample\obj\Debug\net9.0\editorconfigExample.GeneratedMSBuildEditorConfig.editorconfig'.
  O projeto editorconfigExample está usando a configuração de 'C:\Program Files\dotnet\sdk\9.0.311\Sdks\Microsoft.NET.Sdk\analyzers\build\config\analysislevel_9_default.globalconfig'.
  Concluir em 2864 ms.
  Determinando arquivos formatáveis.
  Concluir em 130 ms.
  Executando formatadores.
  Executando a análise de Estilo do Código.
  Determinando os diagnósticos...
  Executando 3 analisadores em editorconfigExample.
  Concluir em 1469 ms.
  Análise concluída em 1470 ms.
  Executando a análise de Referência do Analisador.
  Determinando os diagnósticos...
  Executando 150 analisadores em editorconfigExample.
  Concluir em 257 ms.
  Análise concluída em 257 ms.
  Concluir em 2440 ms.
  0 de 4 arquivos formatados.
  Formatação concluída em 5435 ms.

```

Pudendo detalhar a versão do runtime, o build do sdk e indicando o path do arquivo de configuração do 
editorconfig, além de mostrar o processo de análise e formatação, indicando quais regras estão sendo 
aplicadas e quais arquivos estão sendo formatados ou não.

Quando é bem sucedido e sem o `--verbosity` o comando simplesmente não retorna nada, quando tem o verbosity
ele indica a duração de cada etapa do processo e a quantidade de arquivos formatados, se é usado o 
`--verify-no-changes` ele indica a quantidade de arquivos que não estão seguindo as regras, indicando 
quais regras estão sendo violadas, o que ajuda o desenvolvedor a corrigir o código antes de tentar 
integrar ao branch principal.

Esse projeto serve como exemplo para mostrar o uso do editorconfig e do dotnet format para os projetos
em .net, o comando recomendado para usar depende do ambiente.

Desenvolvimento: `dotnet format --verify-no-changes <solutionPath.sln> --include <folderPath> --verbosity diagnostic`

Integração contínua: `dotnet format --verify-no-changes <solutionPath.sln> --verbosity diagnostic`

Aplicação de regras: `dotnet format <solutionPath.sln> --include <folderPath>`

Exemplo de arquivo YAML para integração contínua no azure pipelines
```yaml
stages:
- stage: build
  jobs:
  - job: "lint"
    pool:
      vmImage: 'ubuntu-latest'

    - task: UseDotNet@2
      inputs:
        packageType: 'sdk'
        version: '9.0.x' # x represent lastest sdk version of 9.0

    # Install dotnet format as a global tool
    - task: DotNetCoreCLI@2
      displayName: 'Install dotnet format'
      inputs:
        command: 'custom'
        custom: 'tool'
        arguments: 'update -g dotnet-format'

    # Run dotnet format --dry-run --check
    # By default, the task ensure the exit code is 0
    # If a file needs to be edited by dotnet format, the exit code will be a non-zero value
    # So the task will fail
    - task: DotNetCoreCLI@2
      displayName: 'Lint dotnet'
      inputs:
        command: 'custom'
        custom: 'format./editorconfigExample.sln'
        arguments: '--verify-no-changes --verbosity diagnostic'
```

|vantagens | desvantagens|
|---|---|
| Fácil de usar, sem necessidade de plugins adicionais. | Pode ser complexo para configurar regras específicas para diferentes linguagens ou projetos. |
| Para .NET, funciona bem com dotnet e versões 6.0 ou superior. | Pode não respeitar as regras de .NET framework se for usado para verificar arquivos em essas versões. |
| Permite definir regras específicas para arquivos ou pastas usando regex. | Pode ser difícil de entender para desenvolvedores que não estão familiarizados com a sintaxe do editorconfig. |
| Para projetos novos é perfeito e para equipes bem estruturadas é ainda melhor | Para projetos legados ou com muitas equipes diferentes, pode ser difícil de aplicar as regras de formatação sem causar muitos conflitos ou problemas de merge ou integração continua. |
| O Visual Studio possui uma interface gráfica para configurar o editorconfig, funciona automático sem plugins adicionais. | Com outros IDE como Rider de JetBrains, pode ter conflitos com algumas regras, é melhor a equipe usar o mesmo IDE para evitar esses problemas. |

## Bibliografia

- [EditorConfig](https://editorconfig.org/)
- [dotnet format](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format)
- [Enforce code Style](https://www.meziantou.net/enforce-dotnet-code-style-in-ci-with-dotnet-format.htm)
- [dotnet format in Azure Pipelines](https://www.meziantou.net/enforce-dotnet-code-style-in-ci-with-dotnet-format.htm#azure-pipelines)
