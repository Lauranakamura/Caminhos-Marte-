﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HashLinear<Tipo> : ITabelaDeHash<Tipo>
      where Tipo : IRegistro<Tipo>, IComparable<Tipo>
{

    private const int TamanhoTabela = 131; 

    private Tipo[] tabela; 
    private bool[] indicesOcupados; 


    public HashLinear()
    {
        tabela = new Tipo[TamanhoTabela];
        indicesOcupados = new bool[TamanhoTabela];
    }


    public List<string> Conteudo()
    {
        List<string> conteudo = new List<string>();

        for (int i = 0; i < TamanhoTabela; i++)
        {
            if (tabela[i] != null)
                conteudo.Add(tabela[i].Chave); //adiciona a chave do elemento a lista
        }
        return conteudo;
    }

  public int Hash(string chave)
  {
        long tot = 0;

        for (int i = 0; i < chave.Length; i++)
            tot += 37 * tot + (char)chave[i];

        tot = tot % tabela.Length;
        if (tot < 0)
            tot += tabela.Length;

        return (int)tot;
    }

  bool ITabelaDeHash<Tipo>.Existe(Tipo item, out int onde)
  {
        onde = Hash(item.Chave); // calcula a posicao inicial usando o hash da chave

        while (indicesOcupados[onde])
        {
            if (item.Chave == tabela[onde].Chave) 
            {
                return true; 
            }

            onde = (onde + 1) % TamanhoTabela;
        }

        return false;
    }

  void ITabelaDeHash<Tipo>.Inserir(Tipo item)
  {
        int onde;

        if (!((ITabelaDeHash<Tipo>)this).Existe(item, out onde))
        {
            tabela[onde] = item;
            indicesOcupados[onde] = true;
        }
    }

  bool ITabelaDeHash<Tipo>.Remover(Tipo item)
  {
        int onde;

        if (((ITabelaDeHash<Tipo>)this).Existe(item, out onde))
        {
            tabela[onde] = default(Tipo);  // remove o item da posição
            indicesOcupados[onde] = false; // marca o índice como desocupado
            return true; 
        }

        return false;
    }

    public Tipo Dado(string chave)
    {
        int pos = Hash(chave);   //calcula a posicao na tabela usando da chave
        Tipo dado = tabela[pos]; //obtem o item na posicao calculada
        if (dado == null)
            throw new Exception("Não foi possivel resgatar dado!");

        return dado;
    }

    public List<Tipo> ConteudoTipo()
    {
        List<Tipo> conteudo = new List<Tipo>();

        for (int i = 0; i < TamanhoTabela; i++)
        {
            if (tabela[i] != null)
                conteudo.Add(tabela[i]); //adiciona o item a lista
        }
        return conteudo;
    }
}

