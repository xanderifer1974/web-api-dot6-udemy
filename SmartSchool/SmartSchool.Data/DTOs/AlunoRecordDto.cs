namespace SmartSchool.Data.DTOs
{
    /// <summary>
    /// Dto utilizado para registrar ou alterar um aluno novo
    /// </summary>
    public class AlunoRecordDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Código de matrícula do aluno dentro da Instituição
        /// </summary>
        public int Matricula { get; set; }
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        /// <summary>
        /// Data que o aluno entrou na Instituição
        /// </summary>
        public DateTime DataInicio { get; set; } = DateTime.Now;
        /// <summary>
        /// Data que o aluno saiu da Instituição. Caso ele ainda estude, esta informação ficará nula
        /// </summary>
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true;
    }
}
