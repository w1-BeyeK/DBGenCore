using EFReplicaCore.Enums;

namespace EFReplicaCore.Models.Helpers
{
    public class ColumnFilter
    {
        public ColumnFilter(string column, object value, FilterType type)
        {
            Column = column;
            Value = value;
            Type = type;
        }

        public ColumnFilter(string column, object value) : this(column, value, FilterType.Equals)
        { }

        public string Column { get; set; }
        public object Value { get; set; }
        public FilterType Type { get; set; }
    }
}
