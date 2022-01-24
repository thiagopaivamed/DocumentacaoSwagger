namespace DocumentacaoSwagger.Models
{
    public class Aviao
    {

        /// <summary>
        /// Identificador do avião
        /// <example> 7 </example>
        /// </summary>
        public int AviaoId { get; set; }

        /// <summary>
        /// Nome do produtor do avião
        /// <example> Boeing </example>
        /// </summary>
        public string NomeProdutor { get; set; }

        /// <summary>
        /// Nome do avião
        /// <example> B737 </example>
        /// </summary>
        public string NomeAviao { get; set; }

        /// <summary>
        /// Quantidade de passageiros que o avião consegue levar
        /// <example> 200 </example>
        /// </summary>
        public int QtdPassageiros{ get; set; }
    }
}
