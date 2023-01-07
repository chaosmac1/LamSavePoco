namespace LamSavePoco;
// ReSharper disable MemberCanBePrivate.Global

public sealed class SavePoco : IDisposable {
    private readonly NPoco.Database Db;
    private bool UseDisposable = false;
    
    public Response Health() {
        try {
            if (Db.Connection is null)
                return Response.Err();
            
            switch (Db.Connection.State) {
                    case ConnectionState.Closed:
                    case ConnectionState.Broken:
                        return Response.Err();
                    case ConnectionState.Open:
                    case ConnectionState.Connecting:
                    case ConnectionState.Executing:
                    case ConnectionState.Fetching:
                        return Response.Ok();
                    default:
                        return Response.Ok();
            }
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response.Err();
        }
#endif
    }
    
    public SavePoco(NPoco.Database db) {
        Db = db;
        UseDisposable = true;
    }

    public SavePoco(NPoco.Database db, bool useDisposable) {
        Db = db;
        UseDisposable = useDisposable;
    }

    public void Dispose() {
        Db.Connection!.Close();
        // Db.KeepConnectionAlive = false;
        // Db.CloseSharedConnection();
        // Db.Dispose();
        // Db.Connection!.Close();
        // Db.Connection!.Dispose();
        // Db.Connection!.Dispose();
    }

    public async Task<Response<object?>> InsertAsync<T>(T poco) {
        try {
            return Response<object?>.Ok(await Db.InsertAsync(poco));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<object?>.Err;
        }
#endif
    }

    public async Task<Response<object?>> InsertAsync<T>(
        string tableName,
        string primaryKeyName,
        bool autoIncrement,
        T poco) {

        try {
            return await Db.InsertAsync(tableName, primaryKeyName, autoIncrement, poco);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<object?>.Err;
        }
#endif
    }

    public async Task<Response> InsertBulkAsync<T>(IEnumerable<T> pocos, InsertBulkOptions? options = null) {
        try {
            await Db.InsertBulkAsync(pocos, options);
            return Response.Ok();
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response.Err();
        }
#endif
    }

    public async Task<Response<int>> InsertBatchAsync<T>(IEnumerable<T> pocos, BatchOptions? options = null) {
        try {
            return Response<int>.Ok(await Db.InsertBatchAsync(pocos, options));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public async Task<Response<int>> UpdateAsync<T>(T poco, Expression<Func<T, object>> fields) {
        try {
            return Response<int>.Ok(await Db.UpdateAsync(poco, fields));
        }

#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public async Task<Response<int>> UpdateAsync(object poco) {
        try {
#pragma warning disable CS8600
            return Response<int>.Ok(await Db.UpdateAsync(poco, null, null));
#pragma warning restore CS8600
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public async Task<Response<int>> UpdateAsync(object poco, IEnumerable<string> columns) {
        try {
#pragma warning disable CS8600
            return Response<int>.Ok(await Db.UpdateAsync(poco, null, columns));
#pragma warning restore CS8600
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public async Task<Response<int>> UpdateAsync(
        object poco,
        object primaryKeyValue,
        IEnumerable<string> columns) {
        try {
            return Response<int>.Ok(await Db.UpdateAsync(poco, primaryKeyValue, columns));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public async Task<Response<int>> UpdateAsync(
        string tableName,
        string primaryKeyName,
        object poco,
        object primaryKeyValue,
        IEnumerable<string> columns) {
        try {
            return Response<int>.Ok(await Db.UpdateAsync(
                tableName,
                primaryKeyName,
                poco,
                primaryKeyValue,
                columns
            ));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public async Task<Response<int>> UpdateBatchAsync<T>(
        IEnumerable<UpdateBatch<T>> pocos,
        BatchOptions? options = null) {

        try {
            return Response<int>.Ok(await Db.UpdateBatchAsync(pocos, options));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public Response<IAsyncUpdateQueryProvider<T>> UpdateManyAsync<T>() {
        try {
            return Response<IAsyncUpdateQueryProvider<T>>.Ok(new AsyncUpdateQueryProvider<T>(Db));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<IAsyncUpdateQueryProvider<T>>.Err;
        }
#endif

    }

    public async Task<Response<int>> DeleteAsync(object poco) {
        try {
            return await Db.DeleteAsync(poco);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public async Task<Response<int>> DeleteAsync(string tableName, string primaryKeyName, object poco) {
        try {
            return await Db.DeleteAsync(tableName, primaryKeyName, poco);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public async Task<Response<int>> DeleteAsync(
        string tableName,
        string primaryKeyName,
        object poco,
        object primaryKeyValue) {
        try {
            return await Db.DeleteAsync(tableName, primaryKeyName, poco, primaryKeyValue);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public Response<IAsyncDeleteQueryProvider<T>> DeleteManyAsync<T>() {
        try {
            return Response<IAsyncDeleteQueryProvider<T>>.Ok(new AsyncDeleteQueryProvider<T>(Db));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<IAsyncDeleteQueryProvider<T>>.Err;
        }
#endif
    }

    public async Task<Response<Page<T>>> PageAsync<T>(long page, long itemsPerPage, Sql sql) {
        return await PageAsync<T>(page, itemsPerPage, sql.SQL, sql.Arguments);
    }

    public async Task<Response<Page<T>>> PageAsync<T>(
        long page,
        long itemsPerPage,
        string sql,
        params object[] args) {
        try {
            return await Db.PageAsync<T>(page, itemsPerPage, sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<Page<T>>.Err;
        }
#endif
    }

    public async Task<Response<List<T>>> FetchAsync<T>(
        long page,
        long itemsPerPage,
        string sql,
        params object[] args) {
        try {
            return await SkipTakeAsync<T>((page - 1L) * itemsPerPage, itemsPerPage, sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<List<T>>.Err;
        }
#endif
    }

    public async Task<Response<List<T>>> FetchAsync<T>(long page, long itemsPerPage, Sql sql) {
        try {
            return await SkipTakeAsync<T>((page - 1L) * itemsPerPage, itemsPerPage, sql.SQL, sql.Arguments);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<List<T>>.Err;
        }
#endif
    }

    public async Task<Response<List<T>>> SkipTakeAsync<T>(
        long skip,
        long take,
        string sql,
        params object[] args) {
        try {
            Db.BuildPageQueries<T>(skip, take, sql, ref args, out var _, out var sqlPage);
            return await FetchAsync<T>(sqlPage, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<List<T>>.Err;
        }
#endif
    }

    public async Task<Response<List<T>>> SkipTakeAsync<T>(long skip, long take, Sql sql) {
        try {
            return await Db.SkipTakeAsync<T>(skip, take, sql.SQL, sql.Arguments);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<List<T>>.Err;
        }
#endif
    }

    public async Task<Response<bool>> IsNewAsync<T>(T poco) {
        try {
            return await Db.IsNewAsync(poco);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<bool>.Err;
        }
#endif
    }

    public async Task<Response> SaveAsync<T>(T poco) {
        try {
            await Db.SaveAsync(poco);
            return Response.Ok();
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response.Err();
        }
#endif
    }

    public async Task<Response<List<T>>> FetchAsync<T>() {
        try {
            return await Db.FetchAsync<T>("", Array.Empty<object>());
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<List<T>>.Err;
        }
#endif
    }

    public async Task<Response<List<T>>> FetchAsync<T>(string sql, params object[] args) {
        try {
            var res = QueryAsync<T>(sql, args);
            if (res == EResponse.Err)
                return Response<List<T>>.Err;
            return await res.Ok().ToListAsync().AsTask();
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<List<T>>.Err;
        }
#endif
    }

    public async Task<Response<List<T>>> FetchAsync<T>(Sql sql) {
        try {
            var res = QueryAsync<T>(sql);
            if (res == EResponse.Err)
                return Response<List<T>>.Err;
            return await res.Ok().ToListAsync().AsTask();
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<List<T>>.Err;
        }
#endif
    }

    public async Task<Response<TRet>> FetchMultipleAsync<T1, T2, TRet>(
        Func<List<T1>, List<T2>, TRet> cb,
        string sql,
        params object[] args) {
        try {
            return await Db.FetchMultipleAsync(cb, sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<TRet>.Err;
        }
#endif
    }

    public async Task<Response<TRet>> FetchMultipleAsync<T1, T2, T3, TRet>(
        Func<List<T1>, List<T2>, List<T3>, TRet> cb,
        string sql,
        params object[] args) {
        try {
            return await Db.FetchMultipleAsync(cb, sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<TRet>.Err;
        }
#endif
    }

    public async Task<Response<TRet>> FetchMultipleAsync<T1, T2, T3, T4, TRet>(
        Func<List<T1>, List<T2>, List<T3>, List<T4>, TRet> cb,
        string sql,
        params object[] args) {
        try {
            return await Db.FetchMultipleAsync(cb, sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<TRet>.Err;
        }
#endif
    }

    public async Task<Response<TRet>> FetchMultipleAsync<T1, T2, TRet>(
        Func<List<T1>, List<T2>, TRet> cb,
        Sql sql) {
        try {
            return await Db.FetchMultipleAsync(cb, sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<TRet>.Err;
        }
#endif
    }

    public async Task<Response<TRet>> FetchMultipleAsync<T1, T2, T3, TRet>(
        Func<List<T1>, List<T2>, List<T3>, TRet> cb,
        Sql sql) {
        try {
            return await Db.FetchMultipleAsync(cb, sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<TRet>.Err;
        }
#endif
    }

    public async Task<Response<TRet>> FetchMultipleAsync<T1, T2, T3, T4, TRet>(
        Func<List<T1>, List<T2>, List<T3>, List<T4>, TRet> cb,
        Sql sql) {
        try {
            return await Db.FetchMultipleAsync(cb, sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<TRet>.Err;
        }
#endif
    }

    public async Task<Response<(List<T1>, List<T2>)>> FetchMultipleAsync<T1, T2>(
        string sql,
        params object[] args) {
        try {
            return await Db.FetchMultipleAsync<T1, T2>(sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<(List<T1>, List<T2>)>.Err;
        }
#endif
    }

    public async Task<Response<(List<T1>, List<T2>, List<T3>)>> FetchMultipleAsync<T1, T2, T3>(
        string sql,
        params object[] args) {
        try {
            return await Db.FetchMultipleAsync<T1, T2, T3>(sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            var ss = Response<(List<T1>, List<T2>, List<T3>)>.Err;
            return ss;
        }
#endif
    }

    public async Task<Response<(List<T1>, List<T2>, List<T3>, List<T4>)>> FetchMultipleAsync<T1, T2, T3, T4>(
        string sql,
        params object[] args) {
        try {
            return await Db.FetchMultipleAsync<T1, T2, T3, T4>(sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<(List<T1>, List<T2>, List<T3>, List<T4>)>.Err;
        }
#endif
    }

    public async Task<Response<(List<T1>, List<T2>)>> FetchMultipleAsync<T1, T2>(Sql sql) {
        try {
            return await Db.FetchMultipleAsync<T1, T2>(sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<(List<T1>, List<T2>)>.Err;
        }
#endif
    }

    public async Task<Response<(List<T1>, List<T2>, List<T3>)>> FetchMultipleAsync<T1, T2, T3>(
        Sql sql) {
        try {
            return await Db.FetchMultipleAsync<T1, T2, T3>(sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<(List<T1>, List<T2>, List<T3>)>.Err;
        }
#endif
    }

    public async Task<Response<(List<T1>, List<T2>, List<T3>, List<T4>)>> FetchMultipleAsync<T1, T2, T3, T4>(
        Sql sql) {
        try {
            return await Db.FetchMultipleAsync<T1, T2, T3, T4>(sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<(List<T1>, List<T2>, List<T3>, List<T4>)>.Err;
        }
#endif
    }

    public Response<IAsyncQueryProviderWithIncludes<T>> QueryAsync<T>() {
        try {
            return Response<IAsyncQueryProviderWithIncludes<T>>.Ok(new AsyncQueryProvider<T>(Db));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<IAsyncQueryProviderWithIncludes<T>>.Err;
        }
#endif
    }

    public Response<IAsyncEnumerable<T>> QueryAsync<T>(string sql, params object[] args) {
        try {
            var res = QueryAsync<T>(new Sql(sql, args));
            return res == EResponse.Err
                ? Response<IAsyncEnumerable<T>>.Err
                : Response<IAsyncEnumerable<T>>.Ok(res.Ok());
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<IAsyncEnumerable<T>>.Err;
        }
#endif
    }

    public Response<IAsyncEnumerable<T>> QueryAsync<T>(Sql sql) {
        try {
            return Response<IAsyncEnumerable<T>>.Ok(Db.QueryAsync<T>(sql));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<IAsyncEnumerable<T>>.Err;
        }
#endif
    }

    public async Task<Response<T>> SingleAsync<T>(string sql, params object[] args) {
        try {
            var res = QueryAsync<T>(sql, args);
            if (res == EResponse.Err)
                return Response<T>.Err;
            return await res.Ok().SingleAsync().AsTask();
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public async Task<Response<T>> SingleAsync<T>(Sql sql) {
        try {
            return await Db.QueryAsync<T>(sql).SingleAsync();
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public async Task<Response<T?>> SingleOrDefaultAsync<T>(string sql, params object[] args) {
        try {
            var res = QueryAsync<T>(sql, args);
            if (res == EResponse.Err)
                return Response<T?>.Err;
            return await res.Ok().SingleOrDefaultAsync();
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T?>.Err;
        }
#endif
    }

    public async Task<Response<T?>> SingleOrDefaultAsync<T>(Sql sql) {
        try {
            var res = QueryAsync<T>(sql);
            if (res == EResponse.Err)
                return Response<T?>.Err;
            return await res.Ok().SingleOrDefaultAsync();
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T?>.Err;
        }
#endif
    }

    public async Task<Response<T>> SingleByIdAsync<T>(object primaryKey) {
        try {
            return await Db.SingleByIdAsync<T>(primaryKey);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public async Task<Response<T>> SingleOrDefaultByIdAsync<T>(object primaryKey) {
        try {
            return await Db.SingleOrDefaultByIdAsync<T>(primaryKey);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public async Task<Response<T>> FirstAsync<T>(string sql, params object[] args) {
        try {
            var res = QueryAsync<T>(sql, args);
            if (res == EResponse.Err)
                return Response<T>.Err;
            return await res.Ok().FirstAsync();
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public async Task<Response<T>> FirstAsync<T>(Sql sql) {
        try {
            var res = QueryAsync<T>(sql);
            if (res == EResponse.Err)
                return Response<T>.Err;
            return await res.Ok().FirstAsync();
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public async Task<Response<T?>> FirstOrDefaultAsync<T>(string sql, params object[] args) {
        try {
            var res = QueryAsync<T>(sql, args);
            if (res == EResponse.Err)
                return Response<T?>.Err;
            return await res.Ok().FirstOrDefaultAsync();
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T?>.Err;
        }
#endif
    }

    public async Task<Response<T?>> FirstOrDefaultAsync<T>(Sql sql) {
        var res = QueryAsync<T>(sql);
        if (res == EResponse.Err)
            return Response<T?>.Err;
        return await res.Ok().FirstOrDefaultAsync();
    }

    public async Task<Response<int>> ExecuteAsync(string sql, params object[] args) {
        try {
            return await ExecuteAsync(new Sql(sql, args));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public async Task<Response<int>> ExecuteAsync(Sql sql) {
        try {
            return await Db.ExecuteAsync(sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public async Task<Response<T>> ExecuteScalarAsync<T>(string sql, params object[] args) {
        try {
            return await ExecuteScalarAsync<T>(new Sql(sql, args));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public async Task<Response<T>> ExecuteScalarAsync<T>(Sql sql) {
        try {
            return await Db.ExecuteScalarAsync<T>(sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }


    public Response<int> Execute(string sql, params object[] args) {
        try {
            return Execute(new Sql(sql, args));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public Response<int> Execute(Sql sql) {
        try {
            return Execute(sql.SQL, CommandType.Text, sql.Arguments);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine("SQL: " + sql.SQL + "\n\n" + e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public Response<int> Execute(string sql, CommandType commandType, params object[] args) {
        try {
            return Db.Execute(sql, commandType, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine("SQL: " + sql + "\n\n" + e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public Response<T> ExecuteScalar<T>(string sql, params object[] args) {
        try {
            return ExecuteScalar<T>(new Sql(sql, args));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public Response<T> ExecuteScalar<T>(Sql sql) {
        try {
            return ExecuteScalar<T>(sql.SQL, CommandType.Text, sql.Arguments);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public Response<T> ExecuteScalar<T>(string sql, CommandType commandType, params object[] args) {
        try {
            return Db.ExecuteScalar<T>(sql, commandType, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public Response<List<T>> Fetch<T>(string sql, params object[] args) {
        try {
            return Fetch<T>(new Sql(sql, args));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<List<T>>.Err;
        }
#endif
    }

    public Response<List<T>> Fetch<T>(Sql sql) {
        try {
            return Db.Query<T>(sql).ToList();
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<List<T>>.Err;
        }
#endif
    }

    public Response<List<T>> Fetch<T>() {
        return Response<List<T>>.Ok(new List<T>(0));
    }


    public Response<Page<T>> Page<T>(long page, long itemsPerPage, Sql sql) {
        try {
            return Db.Page<T>(page, itemsPerPage, sql.SQL, sql.Arguments);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<Page<T>>.Err;
        }
#endif
    }

    public Response<List<T>> Fetch<T>(long page, long itemsPerPage, string sql, params object[] args) {
        try {
            return SkipTake<T>((page - 1L) * itemsPerPage, itemsPerPage, sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<List<T>>.Err;
        }
#endif
    }

    public Response<List<T>> Fetch<T>(long page, long itemsPerPage, Sql sql) {
        try {
            return SkipTake<T>((page - 1L) * itemsPerPage, itemsPerPage, sql.SQL, sql.Arguments);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<List<T>>.Err;
        }
#endif
    }

    public Response<List<T>> SkipTake<T>(long skip, long take, string sql, params object[] args) {
        try {
            Db.BuildPageQueries<T>(skip, take, sql, ref args, out var _, out var sqlPage);
            return Fetch<T>(sqlPage, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<List<T>>.Err;
        }
#endif
    }

    public Response<List<T>> SkipTake<T>(long skip, long take, Sql sql) {
        return SkipTake<T>(skip, take, sql.SQL, sql.Arguments);
    }

    public Response<Dictionary<TKey, TValue>> Dictionary<TKey, TValue>(
        Sql sql) where TKey : notnull {
        try {
            return Dictionary<TKey, TValue>(sql.SQL, sql.Arguments);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<Dictionary<TKey, TValue>>.Err;
        }
#endif
    }

    public Response<Dictionary<TKey, TValue>> Dictionary<TKey, TValue>(
        string sql,
        params object[] args) where TKey : notnull {
        try {
            return Db.Dictionary<TKey, TValue>(sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<Dictionary<TKey, TValue>>.Err;
        }
#endif
    }

    public Response<IEnumerable<T>> Query<T>(string sql, params object[] args) {
        return Query<T>(new Sql(sql, args));
    }

    public Response<IEnumerable<T>> Query<T>(Sql sql) {
        try {
            return Response<IEnumerable<T>>.Ok(Db.Query<T>(sql));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<IEnumerable<T>>.Err;
        }
#endif
    }


    public Response<IQueryProviderWithIncludes<T>> Query<T>() {
        try {
            return Response<IQueryProviderWithIncludes<T>>.Ok(new QueryProvider<T>(Db));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<IQueryProviderWithIncludes<T>>.Err;
        }
#endif
    }

    public Response<(List<T>, List<T1>, List<T2>, List<T3>)> QueryMultiple<T, T1, T2, T3>(
        Func<IQueryProviderWithIncludes<T>, IQueryProvider<T>> query1,
        Func<IQueryProviderWithIncludes<T1>, IQueryProvider<T1>> query2,
        Func<IQueryProviderWithIncludes<T2>, IQueryProvider<T2>> query3,
        Func<IQueryProviderWithIncludes<T3>, IQueryProvider<T3>> query4) {
        try {
            return Db.QueryMultiple(query1, query2, query3, query4);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<(List<T>, List<T1>, List<T2>, List<T3>)>.Err;
        }
#endif
    }

    public Response<List<object>> Fetch(Type type, string sql, params object[] args) {
        return Fetch(type, new Sql(sql, args));
    }

    public Response<List<object>> Fetch(Type type, Sql sql) {
        var res = Query(type, sql);
        if (res == EResponse.Err)
            return Response<List<object>>.Err;
        return res.Ok().ToList();
    }

    public Response<IEnumerable<object>> Query(Type type, string sql, params object[] args) {
        return Query(type, new Sql(sql, args));
    }

    public Response<IEnumerable<object>> Query(Type type, Sql sql) {
        try {
            return Response<IEnumerable<object>>.Ok(Db.Query(type, sql));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<IEnumerable<object>>.Err;
        }
#endif
    }


    public Response<List<T>> FetchOneToMany<T>(Expression<Func<T, IList>> many, Sql sql) {
        try {
            return Db.FetchOneToMany(many, sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<List<T>>.Err;
        }
#endif
    }

    public Response<List<T>> FetchOneToMany<T>(
        Expression<Func<T, IList>> many,
        string sql,
        params object[] args) {
        return FetchOneToMany(many, new Sql(sql, args));
    }

    public Response<List<T>> FetchOneToMany<T>(
        Expression<Func<T, IList>> many,
        Func<T, object> idFunc,
        Sql sql) {
        try {
            return Db.FetchOneToMany(many, idFunc, sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<List<T>>.Err;
        }
#endif
    }

    public Response<List<T>> FetchOneToMany<T>(
        Expression<Func<T, IList>> many,
        Func<T, object> idFunc,
        string sql,
        params object[] args) {
        return FetchOneToMany(many, idFunc, new Sql(sql, args));
    }

    public Response<Page<T>> Page<T>(
        long page,
        long itemsPerPage,
        string sql,
        params object[] args) {
        try {
            return Page<T>(page, itemsPerPage, sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<Page<T>>.Err;
        }
#endif
    }


    public Response<TRet> FetchMultiple<T1, T2, TRet>(
        Func<List<T1>, List<T2>, TRet> cb,
        string sql,
        params object[] args) {
        try {
            return Db.FetchMultiple(cb, sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<TRet>.Err;
        }
#endif
    }

    public Response<TRet> FetchMultiple<T1, T2, T3, TRet>(
        Func<List<T1>, List<T2>, List<T3>, TRet> cb,
        string sql,
        params object[] args) {
        try {
            return Db.FetchMultiple(cb, sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<TRet>.Err;
        }
#endif
    }

    public Response<TRet> FetchMultiple<T1, T2, T3, T4, TRet>(
        Func<List<T1>, List<T2>, List<T3>, List<T4>, TRet> cb,
        string sql,
        params object[] args) {
        try {
            return Db.FetchMultiple(cb, sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<TRet>.Err;
        }
#endif
    }

    public Response<TRet> FetchMultiple<T1, T2, TRet>(Func<List<T1>, List<T2>, TRet> cb, Sql sql) {
        try {
            return Db.FetchMultiple(cb, sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<TRet>.Err;
        }
#endif
    }

    public Response<TRet> FetchMultiple<T1, T2, T3, TRet>(
        Func<List<T1>, List<T2>, List<T3>, TRet> cb,
        Sql sql) {
        try {
            return Db.FetchMultiple(cb, sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<TRet>.Err;
        }
#endif
    }

    public Response<TRet> FetchMultiple<T1, T2, T3, T4, TRet>(
        Func<List<T1>, List<T2>, List<T3>, List<T4>, TRet> cb,
        Sql sql) {
        try {
            return Db.FetchMultiple(cb, sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<TRet>.Err;
        }
#endif
    }

    public Response<(List<T1>, List<T2>)> FetchMultiple<T1, T2>(string sql, params object[] args) {
        try {
            return Db.FetchMultiple<T1, T2>(sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<(List<T1>, List<T2>)>.Err;
        }
#endif
    }

    public Response<(List<T1>, List<T2>, List<T3>)> FetchMultiple<T1, T2, T3>(
        string sql,
        params object[] args) {
        try {
            return Db.FetchMultiple<T1, T2, T3>(sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<(List<T1>, List<T2>, List<T3>)>.Err;
        }
#endif
    }

    public Response<(List<T1>, List<T2>, List<T3>, List<T4>)> FetchMultiple<T1, T2, T3, T4>(
        string sql,
        params object[] args) {
        try {
            return Db.FetchMultiple<T1, T2, T3, T4>(sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<(List<T1>, List<T2>, List<T3>, List<T4>)>.Err;
        }
#endif
    }

    public Response<(List<T1>, List<T2>)> FetchMultiple<T1, T2>(Sql sql) {
        try {
            return Db.FetchMultiple<T1, T2>(sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<(List<T1>, List<T2>)>.Err;
        }
#endif
    }

    public Response<(List<T1>, List<T2>, List<T3>)> FetchMultiple<T1, T2, T3>(Sql sql) {
        try {
            return Db.FetchMultiple<T1, T2, T3>(sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<(List<T1>, List<T2>, List<T3>)>.Err;
        }
#endif
    }

    public Response<(List<T1>, List<T2>, List<T3>, List<T4>)> FetchMultiple<T1, T2, T3, T4>(
        Sql sql) {
        try {
            return Db.FetchMultiple<T1, T2, T3, T4>(sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<(List<T1>, List<T2>, List<T3>, List<T4>)>.Err;
        }
#endif
    }


    public Response<bool> Exists<T>(object primaryKey) {
        try {
            return Db.Exists<T>(primaryKey);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<bool>.Err;
        }
#endif
    }

    public Response<T> SingleById<T>(object primaryKey) {
        try {
            return Db.SingleById<T>(primaryKey);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public Response<T> SingleOrDefaultById<T>(object primaryKey) {
        try {
            return Db.SingleOrDefaultById<T>(primaryKey);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif

    }


    public Response<T> Single<T>(string sql, params object[] args) {
        try {
            return Db.Single<T>(sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public Response<T> SingleInto<T>(T instance, string sql, params object[] args) {
        try {
            return Db.SingleInto(instance, sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public Response<T> SingleOrDefault<T>(string sql, params object[] args) {
        try {
            return Db.SingleOrDefault<T>(sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public Response<T> SingleOrDefaultInto<T>(T instance, string sql, params object[] args) {
        try {
            return Db.SingleOrDefaultInto(instance, sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public Response<T> First<T>(string sql, params object[] args) {
        try {
            return Db.First<T>(sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public Response<T> FirstInto<T>(T instance, string sql, params object[] args) {
        try {
            return Db.FirstInto(instance, sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public Response<T?> FirstOrDefault<T>(string sql, params object[] args) {
        try {
            return Db.FirstOrDefault<T>(sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T?>.Err;
        }
#endif
    }

    public Response<T> FirstOrDefaultInto<T>(T instance, string sql, params object[] args) {
        try {
            return Db.FirstOrDefaultInto(instance, sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public Response<T> Single<T>(Sql sql) {
        try {
            return Db.Single<T>(sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public Response<T> SingleInto<T>(T instance, Sql sql) {
        try {
            return Db.SingleInto(instance, sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public Response<T?> SingleOrDefault<T>(Sql sql) {
        try {
            return Db.SingleOrDefault<T>(sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T?>.Err;
        }
#endif
    }

    public Response<T?> SingleOrDefaultInto<T>(T instance, Sql sql) {
        try {
            return Db.SingleOrDefaultInto(instance, sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T?>.Err;
        }
#endif
    }

    public Response<T> First<T>(Sql sql) {
        try {
            return Db.First<T>(sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public Response<T> FirstInto<T>(T instance, Sql sql) {
        try {
            return Db.FirstInto(instance, sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public Response<T> FirstOrDefault<T>(Sql sql) {
        try {
            return Db.FirstOrDefault<T>(sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public Response<T> FirstOrDefaultInto<T>(T instance, Sql sql) {
        try {
            return Db.FirstOrDefaultInto(instance, sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<T>.Err;
        }
#endif
    }

    public Response<object> Insert<T>(T poco) {
        try {
            return Response<object>.Ok(Db.Insert(poco));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<object>.Err;
        }
#endif
    }

    public Response<object> Insert<T>(string tableName, string primaryKeyName, T poco) {
        try {
            return Response<object>.Ok(Db.Insert(tableName, primaryKeyName, poco));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<object>.Err;
        }
#endif
    }


    public Response<int> InsertBatch<T>(IEnumerable<T> pocos, BatchOptions? options = null) {
        try {
            return Db.InsertBatch(pocos, options);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public Response InsertBulk<T>(IEnumerable<T> pocos, InsertBulkOptions? options = null) {
        try {
            Db.InsertBulk(pocos, options);
            return Response.Ok();
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response.Err();
        }
#endif
    }


    public Response<int> Update(
        string tableName,
        string primaryKeyName,
        object poco,
        object primaryKeyValue) {
#pragma warning disable CS8600
        return Update(tableName, primaryKeyName, poco, primaryKeyValue, null);
#pragma warning restore CS8600
    }

    public Response<int> Update(
        string tableName,
        string primaryKeyName,
        object poco,
        object? primaryKeyValue,
        IEnumerable<string>? columns) {
        try {
            return Db.Update(tableName, primaryKeyName, poco, primaryKeyValue, columns);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public Response<int> UpdateBatch<T>(IEnumerable<UpdateBatch<T>> pocos, BatchOptions? options = null) {
        try {
            return UpdateBatch(pocos, options);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public Response<int> Update(string tableName, string primaryKeyName, object poco) {
#pragma warning disable CS8600
        return Update(tableName, primaryKeyName, poco, null);
#pragma warning restore CS8600
    }

    public Response<int> Update(
        string tableName,
        string primaryKeyName,
        object poco,
        IEnumerable<string>? columns) {
#pragma warning disable CS8600
        return Update(tableName, primaryKeyName, poco, null, columns);
#pragma warning restore CS8600
    }

#pragma warning disable CS8600
    public Response<int> Update(object poco, IEnumerable<string> columns) {
        return Update(poco, null, columns);
    }
#pragma warning restore CS8600

    public Response<int> Update<T>(T poco, Expression<Func<T, object>> fields) {
        try {
            return Db.Update(poco, fields);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public Response<int> Update(object poco) {
#pragma warning disable CS8600
        return Update(poco, null, null);
#pragma warning restore CS8600
    }

    public Response<int> Update(object poco, object primaryKeyValue) {
#pragma warning disable CS8600
        return Update(poco, primaryKeyValue, null);
#pragma warning restore CS8600
    }

    public Response<int> Update(object poco, object? primaryKeyValue, IEnumerable<string>? columns) {
        try {
            return Db.Update(poco, primaryKeyValue, columns);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public Response<int> Update<T>(string sql, params object[] args) {
        try {
            return Db.Update<T>(sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public Response<int> Update<T>(Sql sql) {
        try {
            return Db.Update<T>(sql);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public IDeleteQueryProvider<T> DeleteMany<T>() {
        return new DeleteQueryProvider<T>(Db);
    }

    public Response<int> Delete(string tableName, string primaryKeyName, object poco) {
        try {
#pragma warning disable CS8600
            return Db.Delete(tableName, primaryKeyName, poco, null);
#pragma warning restore CS8600
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public Response<int> Delete(
        string tableName,
        string primaryKeyName,
        object? poco,
        object? primaryKeyValue) {
        try {
            return Db.Delete(tableName, primaryKeyName, poco, primaryKeyValue);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }


    public Response<int> Delete(object poco) {
        try {
            return Db.Delete(poco);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public Response<int> Delete<T>(object pocoOrPrimaryKey) {
        try {
            return Db.Delete<T>(pocoOrPrimaryKey);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public Response<int> Delete<T>(string sql, params object[] args) {
        try {
            return Db.Delete<T>(sql, args);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public Response<int> Delete<T>(Sql sql) {
        try {
            return Response<int>.Ok(Db.Delete<T>(sql));
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<int>.Err;
        }
#endif
    }

    public Response<bool> IsNew<T>(T poco) {
        try {
            return Db.IsNew(poco);
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response<bool>.Err;
        }
#endif
    }

    public Response Save<T>(T poco) {
        try {
            Db.Save(poco);
            return Response.Ok();
        }
#if DEBUG
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
#else
        catch (Exception) {
            return Response.Err();
        }
#endif
    }
}