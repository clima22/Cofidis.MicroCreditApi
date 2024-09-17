I. A API é constituída pelos seguintes projectos:

1. Cofidis.MicroCreditApi.WebApi: é a camada para acesso a API (interface da API)

2. Cofidis.MicroCreditApi.Core.Domain: camada com os componentes Core onde pode constar:
	2.1 As entities do repositórios de dados
	2.2 Definição das interfaces para o acesso ao repositório/base de dados 
	2.2 Constantes
	2.3 Exceptions 

3. Cofidis.MicroCreditApi.Core.Application: camada com a implementação dos usecases ou serviços da API.

4. Cofidis.MicroCreditApi.Infra.ExternalApi: camada para poder consumir serviços externos. Neste exercicio foram definidos 2 serviços externos:
	
	4.1 DigitalMobileKey: simula a api de acesso a chave movel digital (CMD) para obter dados pessoais de um cliente. 
		Para a simulação é feita com base numa lista de clientes configurados no ficheiro de settings da webapi com a chave "DigitalMobileKey"
		
	4.2 CentralCreditRegister: simula a api de acesso ao serviço Central de Responsabilidade de Crédito (CRC) do Banco de Portugal para obter o creditos em nome do cliente.
		A simulação é feita com base numa lista de mapas CRC configurados no ficheiro de settings da webapi com a chave "CentralCreditRegister"

5. Cofidis.MicroCreditApi.Infra.Repository: camada de sismulação de acesso ao repositorio de dados.


II. Validação do limite de credito:  foram definidos alguns parâmetros de configuração de limite quer ao nivel de duração quer ao nivel de montante de crédito.
	A configuração destes parâmetros estão no ficheiro de settings da webapi com a chave "CreditLimit"
	   
III. Cálculo da mensalidade de crédito:

	Foi utilizado a formula básica conhecida como Sistema Francês de Amortização (PMT)
	A taxa de juro a aplicar é anual. (está definida no ficheiro de settings da webapi com a chave "CreditApplication.AnnualInterestRate" com valor 5.0):
	Fórmula: P = (C * i) / (1 - i)^-n  em que:
		P: prestação mensal
		C: montante do empréstimo/credito
		i: taxa de juro mensal (quando a taxa é anual divide-se por 12).
		n: número total de prestações (meses).

IV. Regras para o pedido de credito ser aceite:

	Regra 1. Se o mapa CRC do cliente tiver algum crédito com valor vencido o crédito não é aceite.
	
	Regra 2. Calcular o indice de risco e verificar se está dentro dos limites permitidos.
	
	    Indice de risco é calculado com base na taxa de esforço dos créditos e os indicadores economicos (taxa de inflação e taxa desemprego).
		No mapa de responsabilidade de credito (CRC) existe os valores de crédito por utilizar(Valor Potencial) mas neste exercicio não será levado em conta
		O Credit Score costuma ser um bom indicador para calculo do risco, mas, não será utilizado neste exercicio
		Indice de risco é um valor entre 0 e 1 e quanto menor melhor. 
		
		Fórmula da taxa de esforço: (Total Prestação Mensal Creditos / Redimento Mensal Líquido) / 100
		
		Total Prestação Mensal Creditos: considera-se os creditos devolvidos no mapa CRC mais o valor da mensalidade do crédito que agora quer adquirir na Cofidis
		
		Fórmula Indice de Risco: taxa de esforço * coeficiente de ponderação de taxa de esforço 
                              + taxa de inflação * coeficiente de ponderação de taxa de inflação
                               + taxa desemprego * coeficiente de ponderação de taxa desemprego
						  
		A taxa de inflação e a taxa desemprego utilizado está definido no ficheiro de settings da webapi com a chave "EconomicIndicator":
			"InflationRate": 2.8, //Taxa de inflação em percentagem
			"UnemploymentRate": 6.5 //Taxa de desemprego em percentagem
			
		Os coeficientes de ponderação estão definidos no ficheiro de settings da webapi com a chave "CreditApplication":
			"RiskIndexCoefficient.InflationRate": 0.1,      //Valor do coeficiente para ponderação da taxa de inflação
			"RiskIndexCoefficient.UnemploymentRate": 0.2,  //Valor do coeficiente para ponderação da taxa de desemprego 
			"RiskIndexCoefficient.EffortRate": 0.3,        //Valor do coeficiente para ponderação da taxa de esforço
			
		O Valor máximo do índice de risco permitido para aprovação do crédito está definido no ficheiro de settings da webapi com a chave "CreditApplication":
			"MaxRiskIndexAllowed": 0.28 // Valor máximo do índice de risco permitido para aprovação do crédito
		
V. Script store procedure
	Está na pasta "api/_db"
	
VI: IDE, Framework
	Foi utilizado o Visual Studio 2022 e a framework .NET 8.0
	