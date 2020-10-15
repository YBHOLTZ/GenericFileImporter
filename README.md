# GenericFileImporter
Incompleto.

Falta:

Insert mono-threading
Leitura/Insert multi-threading
Camada DAO (Sql server, Mysql e Oracle)

Produto final.

Capacidade de importar qualquer arquivo (xls, csv e txt) em todos os bancos de dados principais, sem precisar de rebuilds e código extensivo.
A ferramenta vai mapear o layout do arquivo e suas tables por laytous montados em json de forma simplificada.
A formatação de dados será feita em C# como script. Somente as classes de script serão compliladas na execução da aplication.

Para frente, será preparado uma camada para compilação de pequenas lógicas para CRUDS mais específicos.
Inicialmente, será um CRUD burro com formatação de dados.


