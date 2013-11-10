cd F:\GitHub\SQLSAT257MarcoParenzan\SQLSAT257Commands\bin\debug
set-alias installutil $env:windir\Microsoft.NET\Framework64\v4.0.30319\installutil
installutil /u SQLSAT257Commands.dll
installutil SQLSAT257Commands.dll
add-pssnapin SQLSAT257CommandsSnapIn
get-json -Url http://localhost:49222/Activities/Json | Format-Pdf -PdfName .\test.pdf
get-sql -DataSource your-data-source -InitialCatalog SQLSAT257CRM -Query "select * from crm.ActivityViewModels" | where-object {$_.Username == "marco" } | Format-Table
