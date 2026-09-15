using System.Dynamic;

namespace Services.Contracts;

    public interface IDataShaper<T>
    {
        IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> companies, string fieldsString);

        ExpandoObject ShapeData(T company, string fieldsString);
    }
