using System;
using System.Collections.Generic;
using System.Text;
using RegistroDetalle.DAL;
using RegistroDetalle.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace RegistroDetalle.BLL
{
    public class PersonasBll
    {
       

        public static bool Guardar(Personas persona)//Guardar
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                if (contexto.Personas.Add(persona) != null)
                    paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }///fin

        public static Personas Buscar(int id)//            buscar por id
        {
            Contexto contexto = new Contexto();
            Personas persona = new Personas();

            try
            {
                persona = contexto.Personas.Where(p => p.PersonaId == id)
                    .Include(x => x.Telefonos)
                    .SingleOrDefault();

            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return persona;
        }//                       fin
        public static bool Modificar(Personas persona)//modificar
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete FROM TelefonosDetalle Where PersonaId={persona.PersonaId}");
                foreach (var item in persona.Telefonos)
                {
                    
                        contexto.Entry(item).State = EntityState.Added;
                }

               
                contexto.Entry(persona).State = EntityState.Modified;
                paso = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;

        }//                            fin

        public static bool Eliminar(int id)//                    eliminar
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var eliminar = contexto.Personas.Find(id);
                contexto.Entry(eliminar).State = EntityState.Deleted;

                paso = (contexto.SaveChanges() > 0);
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }//                                  fin


        

        public static List<Personas> GetList(Expression<Func<Personas, bool>> persona) //               listar
        {
            List<Personas> lista = new List<Personas>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Personas.Where(persona).ToList();
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }///                               fin

    }
}
