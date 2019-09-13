namespace Pool.Models
{
    public class Bowl
    {
        public int BowlId { get; set; }

        /// <summary>
        /// Загруженность чаши.
        /// </summary>
        public int? WorkLoad { get; set; }
    }
}
