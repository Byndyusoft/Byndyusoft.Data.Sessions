using FluentNHibernate.Mapping;

namespace Byndyusoft.Data.Sessions.Example.Nh
{
    public class PeopleMap : ClassMap<People>
    {
        public PeopleMap()
        {
            Table("peoples");
            DynamicInsert();
            DynamicUpdate();
            BatchSize(10);

            Id(x => x.Id, "id")
                .Access.Property()
                .GeneratedBy.Identity();
            Map(x => x.Name, "name")
                .Not.Nullable()
                .Length(256)
                .Access.Property();
        }
    }
}