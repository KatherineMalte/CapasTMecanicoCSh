using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerMecanico.Entidades;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using TallerMecanico.Datos.Implementacion;
using System.Collections;

namespace TallerMecanico.Datos.Contrato
{
    public class DAOCategoria : IGenericRepository<Categoria>
    {
        string _cadenaConexion;

        public string CadenaConexion
        {
            get
            {
                if (_cadenaConexion == null)
                {
                    _cadenaConexion = ConfigurationManager.
                        ConnectionStrings["cadenaSQL"].ConnectionString;
                }
                return _cadenaConexion;
            }
            set { _cadenaConexion = value; }
        }


         public async Task<List<Categoria>> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ListarCategorias", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Categoria c = new Categoria((int)dr["Id"],
                            (string)dr["Codigo"], (string)dr["Nombre"],
                            (string)dr["Observacion"]);
                        lista.Add(c);
                    }
                }
            }
            return lista;
        }

        //metodo para traer una categoría segun su identificación 
        public async Task<Categoria> TraerPorId(int Id)
        {
            Categoria Categoria = new Categoria();
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("TraerCategoriaPorId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", Id);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr != null && dr.HasRows)
                {
                    dr.Read();
                    Categoria = new Categoria((int)dr["Id"],
                        (string)dr["Codigo"], (string)dr["Nombre"],
                        (string)dr["Observacion"]);
                }
            }
            return Categoria;
        }




        public async Task<bool> Actualizar(Categoria Categoria)
        {
            int n = -1;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ActualizarCategoria", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Categoria.Id);
                cmd.Parameters.AddWithValue("@Codigo", Categoria.Codigo);
                cmd.Parameters.AddWithValue("@Nombre", Categoria.Nombre);
                cmd.Parameters.AddWithValue("@Observacion", Categoria.Observacion);
                n = cmd.ExecuteNonQuery();
                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                    return true;
                else
                    return false;
            }
          //  return n;
        }

        //metodo para eliminar una categoría puntual
        public async Task<bool> Eliminar(int Id)
        {
            int n = -1;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EliminarCategoria", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                n = cmd.ExecuteNonQuery();
                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                    return true;
                else
                    return false;
            }
           // return n;
        }

        public async Task<bool> Insertar(Categoria Categoria)
        {
            int n = -1;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertarCategoria", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Codigo", Categoria.Codigo);
                cmd.Parameters.AddWithValue("@Nombre", Categoria.Nombre);
                cmd.Parameters.AddWithValue("@Observacion", Categoria.Observacion);
                n = cmd.ExecuteNonQuery();
               // return n;
                int filas_afectadas = await cmd.ExecuteNonQueryAsync();
                if (filas_afectadas > 0)
                    return true;
                else
                    return false;
            }
            
        }
    }
}
