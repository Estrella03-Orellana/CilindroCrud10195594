using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilindroCrud10195594
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            //Le indica al sistema que crea la tabla de nuestro contexto
            _connection.CreateTableAsync<Cilindro>();
        }
        //Método para listar los registros de nuestra tabla
        public async Task<List<Cilindro>> GetCilindro()
        {
            return await _connection.Table<Cilindro>().ToListAsync();
        }
        //Método para listar los registros por id
        public async Task<Cilindro> GetById(int id)
        {
            return await _connection.Table<Cilindro>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        //Método para crear registro
        public async Task Create(Cilindro cilindro)
        {
            await _connection.InsertAsync(cilindro);
        }
        //Método para actualzar
        public async Task Update(Cilindro cilindro)
        {
            await _connection.UpdateAsync(cilindro);
        }
        //Método para eliminar
        public async Task Delete(Cilindro cilindro)
        {
            await _connection.DeleteAsync(cilindro);
        }

    }
}
