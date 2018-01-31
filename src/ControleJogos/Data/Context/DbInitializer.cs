namespace ControleJogos.Data.Context
{
    public static class DbInitializer
    {
        public static void Initialize(ControleJogosContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
