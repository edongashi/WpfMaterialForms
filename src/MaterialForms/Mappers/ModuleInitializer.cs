namespace MaterialForms.Mappers
{
    /// <summary>
    /// Module initializer
    /// </summary>
    public static class ModuleInitializer
    {
        /// <summary>
        /// Happens when this module is loaded
        /// </summary>
        public static void Initialize()
        {
            Mapper.InitializeIMapperClasses();
        }
    }
}