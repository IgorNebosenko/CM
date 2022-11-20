namespace CM.Entities
{
    public class EntityFactory
    {
        private InputFactory _inputFactory;
        
        public EntityFactory(InputFactory inputFactory)
        {
            _inputFactory = inputFactory;
        }
        
        public IEntity GetPlayerEntity()
    }
}