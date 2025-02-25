﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceDeskUCAB.Models;
using ServiceDeskUCAB.Models.DTO.DepartamentoDTO;
using ServiceDeskUCAB.Models.DTO.GrupoDTO;
using System.Collections.Generic;
using System.Text;

namespace ServiceDeskUCAB.Servicios.ModuloDepartamento
{
    public class ServicioDepartamento_API : IServicioDepartamento_API
	{
        //Declaracion de variables
        private static string _baseUrl;
        private JObject _json_respuesta;

        //Constructor
        public ServicioDepartamento_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
			Console.WriteLine(builder.ToString());
            _baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

		//Eliminar un departamento seleccionado
		public async Task<JObject> EliminarDepartamento(Guid id)
        {
			HttpClient cliente = new()
			{
				BaseAddress = new Uri(_baseUrl)
			};
			var response = await cliente.DeleteAsync($"Departamento/EliminarDepartamento/{id}");

			var respuesta = await response.Content.ReadAsStringAsync();
			JObject json_respuesta = JObject.Parse(respuesta);

			return json_respuesta;
		}

		//Carga la lista de departamentos y grupos
		public async Task<Tuple<List<DepartamentoDto>, List<GrupoDto>>> ListaDepartamentoGrupo()
        {
            List<DepartamentoDto> listaDepartamento = new List<DepartamentoDto>();
            List<GrupoDto> listaGrupo = new List<GrupoDto>();

            var cliente = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };

            try
            {
                var responseDept = await cliente.GetAsync("Departamento/ConsultarDepartamentoNoEliminado");
                var responseGrupo = await cliente.GetAsync("Grupo/ConsultarGrupoNoEliminado/");

                if (responseDept.IsSuccessStatusCode && responseGrupo.IsSuccessStatusCode)
                {
                    var respuestaDept = await responseDept.Content.ReadAsStringAsync();
                    JObject json_respuestaDept = JObject.Parse(respuestaDept);

                    var respuestaGrupo = await responseGrupo.Content.ReadAsStringAsync();
                    JObject json_respuestaGrupo = JObject.Parse(respuestaGrupo);


                    //Obtengo la data del json respuesta Departamento
                    string stringDataRespuestaDept = json_respuestaDept["data"].ToString();
                    var resultadoDept = JsonConvert.DeserializeObject<List<DepartamentoDto>>(stringDataRespuestaDept);

                    //Obtengo la data del json respuesta Grupo
                    string stringDataRespuestaGrupo = json_respuestaGrupo["data"].ToString();
                    var resultadoGrupo = JsonConvert.DeserializeObject<List<GrupoDto>>(stringDataRespuestaGrupo);


                    listaDepartamento = resultadoDept;
                    listaGrupo = resultadoGrupo;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"ERROR de conexión con la API: '{ex.Message}'");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            var tupla = new Tuple<List<DepartamentoDto>, List<GrupoDto>>(listaDepartamento, listaGrupo);

            return tupla;
        }

        //Almacenar la información de un nuevo departamento
		public async Task<JObject> RegistrarDepartamento(DepartamentoDto departamento)
		{
			HttpClient cliente = new()
			{
				BaseAddress = new Uri(_baseUrl)
			};

			var content = new StringContent(JsonConvert.SerializeObject(departamento), Encoding.UTF8, "application/json");
			Console.WriteLine(JsonConvert.SerializeObject(departamento));

			try
			{
				var response = await cliente.PostAsync("Departamento/CrearDepartamento/", content);
				var respuesta = await response.Content.ReadAsStringAsync();
				JObject _json_respuesta = JObject.Parse(respuesta);

				return _json_respuesta;
			}
			catch (HttpRequestException ex)
			{
				Console.WriteLine($"ERROR de conexión con la API: '{ex.Message}'");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			return _json_respuesta;
		}

		//Modificar los campos de un departamento
		public async Task<DepartamentoModel> MostrarInfoDepartamento(Guid id)
		{
			DepartamentoModel departamento = new DepartamentoModel();

			HttpClient cliente = new()
			{
				BaseAddress = new Uri(_baseUrl)
			};

			try
			{
				var responseDept = await cliente.GetAsync($"Departamento/ConsultarDepartamentoPorID/{id}");

				if (responseDept.IsSuccessStatusCode)
				{
					var respuestaDept = await responseDept.Content.ReadAsStringAsync();
					JObject json_respuestaDept = JObject.Parse(respuestaDept);

					string stringDataRespuestaDept = json_respuestaDept["data"].ToString();
					var resultadoDept = JsonConvert.DeserializeObject<DepartamentoModel>(stringDataRespuestaDept);
					departamento = resultadoDept;
				}
			}
			catch (Exception ex)
			{
				throw ex.InnerException!;
			}
			return departamento;
		}

		public async Task<JObject> EditarDepartamento(DepartamentoDto_Update dept)
		{
			HttpClient cliente = new()
			{
				BaseAddress = new Uri(_baseUrl)
			};

			var content = new StringContent(JsonConvert.SerializeObject(dept), Encoding.UTF8, "application/json");

			try
			{
				var response = await cliente.PutAsync("Departamento/ActualizarDepartamento", content);
				var respuesta = await response.Content.ReadAsStringAsync();
				JObject _json_respuesta = JObject.Parse(respuesta);
				return _json_respuesta;

			}
			catch (HttpRequestException ex)
			{
				Console.WriteLine($"ERROR de conexión con la API: '{ex.Message}'");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			return _json_respuesta;
		}

		public async Task<List<DepartamentoModel>> DepartamentoAsociadoGrupo(Guid id)
		{
			DepartamentoModel departamento = new DepartamentoModel();

			HttpClient cliente = new()
			{
				BaseAddress = new Uri(_baseUrl)
			};

			try
			{
				var responseDept = await cliente.GetAsync($"Departamento/ConsultarDepartamentosPorIdGrupo/{id}");

				if (responseDept.IsSuccessStatusCode)
				{
					var respuestaDept = await responseDept.Content.ReadAsStringAsync();
					JObject json_respuestaDept = JObject.Parse(respuestaDept);

					string stringDataRespuestaDept = json_respuestaDept["data"].ToString();
					var resultadoDept = JsonConvert.DeserializeObject <List<DepartamentoModel>>(stringDataRespuestaDept);
					departamento.departamentos = resultadoDept;
				}


			}
			catch (Exception ex)
			{
					throw ex.InnerException!;
			}
			return departamento.departamentos;
		}
	}
}
