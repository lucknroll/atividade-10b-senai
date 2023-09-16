using Npgsql;

namespace Projeto_Web_Lh_Pets_versão_1
{
    class Banco
    {   
	
    private List<Clientes> lista = new List<Clientes>();

    public List<Clientes> GetLista()
    {
        return lista;
    }
    
	public Banco()
	{
	 	try
                {
                    NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder
                    {
                        Host = "localhost",
                        Port = 5432,
                        Username = "postgres",
                        Password = "postgres",
                        Database = "vendas"
                    };

                    using (NpgsqlConnection conexao = new NpgsqlConnection(builder.ConnectionString))
                    {
                        String sql = "SELECT * FROM tblclientes";
                        using (NpgsqlCommand comando = new NpgsqlCommand(sql, conexao))
                        {
                            conexao.Open();
                            using (NpgsqlDataReader tabela = comando.ExecuteReader())
                            {

                                while(tabela.Read())
                                {
                                    lista.Add(new Clientes()
                                    {
                                        cpf_cnpj = tabela["cpf_cnpj"].ToString(),
                                        nome = tabela["nome"].ToString(),
                                        endereco = tabela["endereco"].ToString(),
                                        rg_ie = tabela["rg_ie"].ToString(),
                                        tipo = tabela["tipo"].ToString(),
                                        valor = (float)Convert.ToDecimal(tabela["valor"]),
                                        valor_imposto = (float)Convert.ToDecimal(tabela["valor_imposto"]),
                                        total = (float)Convert.ToDecimal(tabela["total"])
                                    });
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
	}
	
  
 
	public String GetListaString()
	{
		string enviar= "<!DOCTYPE html>\n<html>\n<head>\n<meta charset='utf-8' />\n"+
                      "<title>Cadastro de Clientes</title>\n</head>\n<body>";
        enviar = enviar + "<b>   CPF / CNPJ    -      Nome    -    Endereço    -   RG / IE   -   Tipo  -   Valor   - Valor Imposto -   Total  </b>";

        int i=0;
        string corfundo="",cortexto="";

		foreach (Clientes cli in GetLista())
                {

                    if (i % 2 == 0)
                             {   corfundo ="#6f47ff"; cortexto="white";}
                            else
                              {  corfundo = "#ffffff";cortexto="#6f47ff";}
                            i++;


                    enviar = enviar + 
                   $"\n<br><div style='background-color:{corfundo};color:{cortexto};'>" +
                    cli.cpf_cnpj + " - " + 
                    cli.nome + " - " + cli.endereco + " - " + cli.rg_ie + " - " +
                    cli.tipo + " - " + cli.valor.ToString("C") + " - " + 
                    cli.valor_imposto.ToString("C") + " - " + cli.total.ToString("C") + "<br>"+
                     "</div>";
                }
		return enviar;
	}

	public void imprimirListaConsole(){

                Console.WriteLine("   CPF / CNPJ   " + " - " + "    Nome   " + 
                    " - " + "   Endereço   " + " - " + "  RG / IE  " + " - " +
                    "  Tipo " + " - " + "  Valor  " + " - " + "Valor Imposto" + 
                    " - " + "  Total  ");

                foreach (Clientes cli in GetLista())
                {
                    Console.WriteLine(cli.cpf_cnpj + " - " + 
                    cli.nome + " - " + cli.endereco + " - " + cli.rg_ie + " - " +
                    cli.tipo + " - " + cli.valor.ToString("C") + " - " + 
                    cli.valor_imposto.ToString("C") + " - " + cli.total.ToString("C"));
                }
        }

        
    }
}