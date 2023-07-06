using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PortalDeCiclismo.Models;

namespace PortalDeCiclismo.DAL
{
    public class PortalInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PortalContext>
    {
        protected override void Seed(PortalContext context)
        {
            var acessos = new List<Acesso>
            {
            new Acesso{UsuarioID=1, Usuario="rey@gmail.com", Senha="leal", Tipo=1, ID=1},
            new Acesso{UsuarioID=2, Usuario="fer@gmail.com", Senha="morais", Tipo=1, ID=2},
            new Acesso{UsuarioID=3, Usuario="guto@educa.com", Senha="guto", Tipo=2, ID=1}
            };
            acessos.ForEach(s => context.Acessos.Add(s));
            context.SaveChanges();

            var atletas = new List<Atleta>
            {
            new Atleta{Nome="Reinaldo Leal de Souza", Apelido="Rey", Email="rey@gmail.com", DataDeNascimento=DateTime.Parse("2018-09-01"), Peso=82, Altura=181, Sexo=Sexo.Masculino},
            new Atleta{Nome="Fernanda Morais da Nobrega Guilherme", Apelido="Nanda", Email="fer@gmail.com", DataDeNascimento=DateTime.Parse("2018-09-01"), Peso=86, Altura=176, Sexo=Sexo.Feminino},
            new Atleta{Nome="Joyce Mafra da Silva", Apelido="Joyce", Email="joyceclickdisk@gmail.com", DataDeNascimento=DateTime.Parse("2018-09-01"), Peso=69, Altura=169, Sexo=Sexo.Feminino},
            new Atleta{Nome="Sirlene Costa", Apelido="Sirlene", Email="silahistoria@yahoo.com.br", DataDeNascimento=DateTime.Parse("2019-01-01"), Peso=57, Altura=158, Sexo=Sexo.Feminino},
            new Atleta{Nome="Roberto Junior Sato", Apelido="Juninho", Email="jr@delaval.com.br", DataDeNascimento=DateTime.Parse("2019-02-12"), Peso=73, Altura=172, Sexo=Sexo.Masculino},
            new Atleta{Nome="Rogerio Scatolin", Apelido="Bola", Email="bola@gmail.com", DataDeNascimento=DateTime.Parse("2019-03-23"), Peso=88, Altura=177, Sexo=Sexo.Masculino},
            new Atleta{Nome="Milton Henrique Brandão", Apelido="Miltão", Email="miltonkom@gmail.com", DataDeNascimento=DateTime.Parse("2019-04-05"), Peso=89, Altura=192, Sexo=Sexo.Masculino},
            new Atleta{Nome="Helder Castro Grael", Apelido="Heldinho", Email="heldinhosariema@msn.com", DataDeNascimento=DateTime.Parse("2019-04-22"), Peso=73, Altura=178, Sexo=Sexo.Masculino}
            };
            atletas.ForEach(s => context.Atletas.Add(s));
            context.SaveChanges();

            var categorias = new List<Categoria>
            {
            new Categoria{CategoriaID=1, Titulo="Base", Descricao="Fortalecimento e resistência"},
            new Categoria{CategoriaID=2, Titulo="Intervalado", Descricao="Disparos curtos para aumentar a potência anaeróbica"},
            new Categoria{CategoriaID=3, Titulo="XCO", Descricao="Pista técnica e de grande esforço"},
            new Categoria{CategoriaID=4, Titulo="Força", Descricao="Específico para desenvolvimento de força"},
            new Categoria{CategoriaID=5, Titulo="Técnica", Descricao="Obstáculos diversificados para desenvolvimento de descisões rápidas"},
            new Categoria{CategoriaID=6, Titulo="Iniciante", Descricao="Básico para recém egressos"}
            };
            categorias.ForEach(s => context.Categorias.Add(s));
            context.SaveChanges();
            
            var tecnicos = new List<Tecnico>
            {
            new Tecnico{TecnicoID=1, Nome="Guto Ferreira Costa", Especialidade="Educação Ciclistica", Email="guto@educa.com"},
            new Tecnico{TecnicoID=2, Nome="Henrique Avancinni", Especialidade="Consultoria Desportiva", Email="avancinni@hotmail.com"}
            };
            tecnicos.ForEach(s => context.Tecnicos.Add(s));
            context.SaveChanges();

            var treinos = new List<Treino>
            {
            new Treino{AtletaID=1, CategoriaID=1, TecnicoID=1, Desempenho=Desempenho.A, Observacao="Concluiu com sucesso o treino"},
            new Treino{AtletaID=1, CategoriaID=2, TecnicoID=1, Desempenho=Desempenho.C, Observacao="Treino satisfatório, focar mais no desempenho"},
            new Treino{AtletaID=1, CategoriaID=3, TecnicoID=1, Desempenho=Desempenho.B, Observacao="Problemas mecânicos não foi possível completar"},
            new Treino{AtletaID=2, CategoriaID=4, TecnicoID=1, Desempenho=Desempenho.B, Observacao="Se possível marcar consulta com nutricionista desportivo"},
            new Treino{AtletaID=2, CategoriaID=5, TecnicoID=2, Desempenho=Desempenho.E, Observacao="Péssimo a regular, reiniciar os procedimentos de treinos"},
            new Treino{AtletaID=2, CategoriaID=3, TecnicoID=1, Desempenho=Desempenho.E, Observacao="Regular, atleta não participou ativamente"},
            new Treino{AtletaID=3, CategoriaID=6, TecnicoID=1, Observacao="NA"},
            new Treino{AtletaID=4, CategoriaID=1, TecnicoID=2, Observacao="NA"},
            new Treino{AtletaID=4, CategoriaID=2, TecnicoID=2, Desempenho=Desempenho.E, Observacao="Falta de concentração e foco"},
            new Treino{AtletaID=5, CategoriaID=3, TecnicoID=1, Desempenho=Desempenho.C, Observacao="Satisfatório"},
            new Treino{AtletaID=6, CategoriaID=4, TecnicoID=1, Observacao="NA"},
            new Treino{AtletaID=7, CategoriaID=5, TecnicoID=1, Desempenho=Desempenho.A, Observacao="Ótimo treino, atleta concluiu todas etapas"}
            };
            treinos.ForEach(s => context.Treinos.Add(s));
            context.SaveChanges();

            var etapasDeTreinos = new List<EtapaDeTreino>
            {
                new EtapaDeTreino{TreinoID=1, Descricao="Etapa 1 - Alongamentos"},
                new EtapaDeTreino{TreinoID=1, Descricao="Etapa 2 - Aquecimentos"},
                new EtapaDeTreino{TreinoID=1, Descricao="Etapa 3 - Execução"},
                new EtapaDeTreino{TreinoID=2, Descricao="Etapa 1 - Alongamentos"},
                new EtapaDeTreino{TreinoID=2, Descricao="Etapa 2 - Aquecimentos"},
                new EtapaDeTreino{TreinoID=2, Descricao="Etapa 3 - Execução"},
                new EtapaDeTreino{TreinoID=3, Descricao="Etapa 1 - Alongamentos"},
                new EtapaDeTreino{TreinoID=3, Descricao="Etapa 2 - Aquecimentos"},
                new EtapaDeTreino{TreinoID=3, Descricao="Etapa 3 - Execução"},
                new EtapaDeTreino{TreinoID=4, Descricao="Etapa 1 - Alongamentos"},
                new EtapaDeTreino{TreinoID=4, Descricao="Etapa 2 - Aquecimentos"},
                new EtapaDeTreino{TreinoID=4, Descricao="Etapa 3 - Execução"},
                new EtapaDeTreino{TreinoID=5, Descricao="Etapa 1 - Alongamentos"},
                new EtapaDeTreino{TreinoID=6, Descricao="Etapa 1 - Alongamentos"},
                new EtapaDeTreino{TreinoID=7, Descricao="Etapa 1 - Alongamentos"},
                new EtapaDeTreino{TreinoID=8, Descricao="Etapa 1 - Alongamentos"},
                new EtapaDeTreino{TreinoID=9, Descricao="Etapa 1 - Alongamentos"},
                new EtapaDeTreino{TreinoID=10, Descricao="Etapa 1 - Alongamentos"},
                new EtapaDeTreino{TreinoID=11, Descricao="Etapa 1 - Alongamentos"},
                new EtapaDeTreino{TreinoID=12, Descricao="Etapa 1 - Alongamentos"}
            };
            etapasDeTreinos.ForEach(s => context.EtapasDeTreino.Add(s));
            context.SaveChanges();
        }
    }
}