using Calempus360.Errors.CustomExceptions;
using Microsoft.Data.SqlClient;

namespace Calempus360.Errors.Mappers;

public static class SqlExceptionMapper
{
    public static void MapSqlException(this SqlException ex)
    {
        switch (ex.Number)
        {
            case 547:
                throw new ExistingEntityException(
                    "The entity you are trying to delete is being used in another entity.");
            case 2601:
            case 2627:
                throw new ExistingEntityException("The entity or at least one entity's field you are trying to add already exists.");
            default:
                throw new Exception(ex.Message);
        }
    }
}