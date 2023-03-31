//this was autogenerated by a tool. Do not modify! Use partial
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Generated;
[ApiController]
[Route("api/[controller]/[action]")]    
public partial class AdvancedSearch_ApplicationDBContext_ModifiedUserFileController : Controller
{
    private ISearchDataModifiedUserFile _search;
    public AdvancedSearch_ApplicationDBContext_ModifiedUserFileController(ISearchDataModifiedUserFile search)
	{
        _search=search;
	}
    [HttpGet]
    public async Task<long> GetAllCount()
    {
       return await _search.GetAllCount();
        
    }
    
    [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> GetAll()
    {
        await foreach(var item in _search.ModifiedUserFileFind_AsyncEnumerable(null))
        {
            yield return (ModifiedUserFile_Table)item!;
        }
        
    }
    [HttpGet]   
    public async IAsyncEnumerable<ModifiedUserFile_Table> GetSearchSimple(string ColumnName, string Operator, string Value){
           var search = new SearchModifiedUserFile();
           search.PageSize = int.MaxValue - 1;
        search.SearchFields = new SearchField<eModifiedUserFileColumns>[1];
        search.SearchFields[0]= new SearchField<eModifiedUserFileColumns>();
        if(Enum.TryParse< eModifiedUserFileColumns >(ColumnName,true ,out var valField)){
            search.SearchFields[0].FieldName = valField;
        }
        else
        {
            search.SearchFields[0].FieldName = (eModifiedUserFileColumns )int.Parse(ColumnName);;
        }
        search.SearchFields[0].Value= Value;
        var criteria= SearchCriteria.None;
        if(Enum.TryParse<SearchCriteria>(Operator,true,out var value))
        {
            criteria = value;
        }
        else
        {
            criteria = (SearchCriteria)int.Parse(Operator);
        }
        
        search.SearchFields[0].Criteria= criteria;
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(search))
        {
            yield return (ModifiedUserFile_Table)item!;
        }

    }
    [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> GetWithSearch(SearchModifiedUserFile s)
    {
        await foreach(var item in _search.ModifiedUserFileFind_AsyncEnumerable(s))
        {
            yield return (ModifiedUserFile_Table)item!;
        }
        
    }
    [HttpGet]
    public async Task<long> GetWithSearchCount(SearchModifiedUserFile? s)
    {
        if (s == null)
            return await GetAllCount();

        return await _search.GetAllCount(s);
    }

//has one key
    [HttpGet]
    public async Task<ModifiedUserFile_Table?> GetSingle(long id){
        var data=await _search.ModifiedUserFileGetSingle(id);
       if(data == null)
        return null;
       return (ModifiedUserFile_Table)data;
    }

        
    [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDUser_EqualValue( long  value)
    {
        var sc = GeneratorFromDB.SearchCriteria.Equal;
        await foreach (var item in _search.ModifiedUserFileSimpleSearch_IDUser(sc, value))
        {
            yield return (ModifiedUserFile_Table)item!;
        }
    }
    [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDUser_DifferentValue( long  value)
    {
        var sc = GeneratorFromDB.SearchCriteria.Different;
        await foreach (var item in _search.ModifiedUserFileSimpleSearch_IDUser(sc, value))
        {
            yield return (ModifiedUserFile_Table)item!;
        }
    }
    [HttpGet]
    public  async IAsyncEnumerable<ModifiedUserFile_Table> IDUser_SimpleSearch(GeneratorFromDB.SearchCriteria sc,  long value){
        await foreach(var item in _search.ModifiedUserFileSimpleSearch_IDUser(sc,value))
        {
            yield return (ModifiedUserFile_Table)item!;
        }
    }

         
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDUser_EqualValues( long[]  values)
    {
        string? value=null;
        if(values.Length>0)
            value=string.Join( ",",values);
        var sc=SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.InArray,eModifiedUserFileColumns.IDUser,value);
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {
        
            yield return (ModifiedUserFile_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDUser_DifferentValues( long[]  values)
    {
        string? value=null;
        if(values.Length>0)
            value=string.Join( ",",values);
        var sc=SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.NotInArray,eModifiedUserFileColumns.IDUser,value);
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {
        
            yield return (ModifiedUserFile_Table)item!;
        }
    }
              [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDUser_LessOrEqual(long  val)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.LessOrEqual, eModifiedUserFileColumns.IDUser  , val.ToString());
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDUser_Less(long  val)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.Less, eModifiedUserFileColumns.IDUser  , val.ToString());
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }
     
      [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDUser_GreaterOrEqual(long  val)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.GreaterOrEqual, eModifiedUserFileColumns.IDUser  , val.ToString());
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDUser_Greater(long  val)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.Greater, eModifiedUserFileColumns.IDUser  , val.ToString());
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDUser_Between( long  valStart, long valEnd)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.Between, eModifiedUserFileColumns.IDUser, valStart +","+ valEnd);
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }    

    [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDUser_NotBetween( long  valStart, long valEnd)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.NotBetween, eModifiedUserFileColumns.IDUser, valStart +","+ valEnd);
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }    

        
    [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDFile_EqualValue( long  value)
    {
        var sc = GeneratorFromDB.SearchCriteria.Equal;
        await foreach (var item in _search.ModifiedUserFileSimpleSearch_IDFile(sc, value))
        {
            yield return (ModifiedUserFile_Table)item!;
        }
    }
    [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDFile_DifferentValue( long  value)
    {
        var sc = GeneratorFromDB.SearchCriteria.Different;
        await foreach (var item in _search.ModifiedUserFileSimpleSearch_IDFile(sc, value))
        {
            yield return (ModifiedUserFile_Table)item!;
        }
    }
    [HttpGet]
    public  async IAsyncEnumerable<ModifiedUserFile_Table> IDFile_SimpleSearch(GeneratorFromDB.SearchCriteria sc,  long value){
        await foreach(var item in _search.ModifiedUserFileSimpleSearch_IDFile(sc,value))
        {
            yield return (ModifiedUserFile_Table)item!;
        }
    }

         
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDFile_EqualValues( long[]  values)
    {
        string? value=null;
        if(values.Length>0)
            value=string.Join( ",",values);
        var sc=SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.InArray,eModifiedUserFileColumns.IDFile,value);
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {
        
            yield return (ModifiedUserFile_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDFile_DifferentValues( long[]  values)
    {
        string? value=null;
        if(values.Length>0)
            value=string.Join( ",",values);
        var sc=SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.NotInArray,eModifiedUserFileColumns.IDFile,value);
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {
        
            yield return (ModifiedUserFile_Table)item!;
        }
    }
              [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDFile_LessOrEqual(long  val)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.LessOrEqual, eModifiedUserFileColumns.IDFile  , val.ToString());
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDFile_Less(long  val)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.Less, eModifiedUserFileColumns.IDFile  , val.ToString());
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }
     
      [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDFile_GreaterOrEqual(long  val)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.GreaterOrEqual, eModifiedUserFileColumns.IDFile  , val.ToString());
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDFile_Greater(long  val)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.Greater, eModifiedUserFileColumns.IDFile  , val.ToString());
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDFile_Between( long  valStart, long valEnd)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.Between, eModifiedUserFileColumns.IDFile, valStart +","+ valEnd);
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }    

    [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> IDFile_NotBetween( long  valStart, long valEnd)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.NotBetween, eModifiedUserFileColumns.IDFile, valStart +","+ valEnd);
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }    

        
    [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ModifiedDate_EqualValue( DateTime  value)
    {
        var sc = GeneratorFromDB.SearchCriteria.Equal;
        await foreach (var item in _search.ModifiedUserFileSimpleSearch_ModifiedDate(sc, value))
        {
            yield return (ModifiedUserFile_Table)item!;
        }
    }
    [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ModifiedDate_DifferentValue( DateTime  value)
    {
        var sc = GeneratorFromDB.SearchCriteria.Different;
        await foreach (var item in _search.ModifiedUserFileSimpleSearch_ModifiedDate(sc, value))
        {
            yield return (ModifiedUserFile_Table)item!;
        }
    }
    [HttpGet]
    public  async IAsyncEnumerable<ModifiedUserFile_Table> ModifiedDate_SimpleSearch(GeneratorFromDB.SearchCriteria sc,  DateTime value){
        await foreach(var item in _search.ModifiedUserFileSimpleSearch_ModifiedDate(sc,value))
        {
            yield return (ModifiedUserFile_Table)item!;
        }
    }

         
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ModifiedDate_EqualValues( DateTime[]  values)
    {
        string? value=null;
        if(values.Length>0)
            value=string.Join( ",",values);
        var sc=SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.InArray,eModifiedUserFileColumns.ModifiedDate,value);
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {
        
            yield return (ModifiedUserFile_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ModifiedDate_DifferentValues( DateTime[]  values)
    {
        string? value=null;
        if(values.Length>0)
            value=string.Join( ",",values);
        var sc=SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.NotInArray,eModifiedUserFileColumns.ModifiedDate,value);
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {
        
            yield return (ModifiedUserFile_Table)item!;
        }
    }
              [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ModifiedDate_LessOrEqual(DateTime  val)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.LessOrEqual, eModifiedUserFileColumns.ModifiedDate  , val.ToString());
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ModifiedDate_Less(DateTime  val)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.Less, eModifiedUserFileColumns.ModifiedDate  , val.ToString());
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }
     
      [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ModifiedDate_GreaterOrEqual(DateTime  val)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.GreaterOrEqual, eModifiedUserFileColumns.ModifiedDate  , val.ToString());
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ModifiedDate_Greater(DateTime  val)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.Greater, eModifiedUserFileColumns.ModifiedDate  , val.ToString());
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ModifiedDate_Between( DateTime  valStart, DateTime valEnd)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.Between, eModifiedUserFileColumns.ModifiedDate, valStart +","+ valEnd);
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }    

    [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ModifiedDate_NotBetween( DateTime  valStart, DateTime valEnd)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.NotBetween, eModifiedUserFileColumns.ModifiedDate, valStart +","+ valEnd);
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }    

        
    [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ID_EqualValue( long  value)
    {
        var sc = GeneratorFromDB.SearchCriteria.Equal;
        await foreach (var item in _search.ModifiedUserFileSimpleSearch_ID(sc, value))
        {
            yield return (ModifiedUserFile_Table)item!;
        }
    }
    [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ID_DifferentValue( long  value)
    {
        var sc = GeneratorFromDB.SearchCriteria.Different;
        await foreach (var item in _search.ModifiedUserFileSimpleSearch_ID(sc, value))
        {
            yield return (ModifiedUserFile_Table)item!;
        }
    }
    [HttpGet]
    public  async IAsyncEnumerable<ModifiedUserFile_Table> ID_SimpleSearch(GeneratorFromDB.SearchCriteria sc,  long value){
        await foreach(var item in _search.ModifiedUserFileSimpleSearch_ID(sc,value))
        {
            yield return (ModifiedUserFile_Table)item!;
        }
    }

         
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ID_EqualValues( long[]  values)
    {
        string? value=null;
        if(values.Length>0)
            value=string.Join( ",",values);
        var sc=SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.InArray,eModifiedUserFileColumns.ID,value);
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {
        
            yield return (ModifiedUserFile_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ID_DifferentValues( long[]  values)
    {
        string? value=null;
        if(values.Length>0)
            value=string.Join( ",",values);
        var sc=SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.NotInArray,eModifiedUserFileColumns.ID,value);
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {
        
            yield return (ModifiedUserFile_Table)item!;
        }
    }
              [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ID_LessOrEqual(long  val)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.LessOrEqual, eModifiedUserFileColumns.ID  , val.ToString());
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ID_Less(long  val)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.Less, eModifiedUserFileColumns.ID  , val.ToString());
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }
     
      [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ID_GreaterOrEqual(long  val)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.GreaterOrEqual, eModifiedUserFileColumns.ID  , val.ToString());
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ID_Greater(long  val)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.Greater, eModifiedUserFileColumns.ID  , val.ToString());
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }
     [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ID_Between( long  valStart, long valEnd)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.Between, eModifiedUserFileColumns.ID, valStart +","+ valEnd);
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }    

    [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> ID_NotBetween( long  valStart, long valEnd)
    {
        var sc = SearchModifiedUserFile.FromSearch(GeneratorFromDB.SearchCriteria.NotBetween, eModifiedUserFileColumns.ID, valStart +","+ valEnd);
        await foreach (var item in _search.ModifiedUserFileFind_AsyncEnumerable(sc))
        {

            yield return (ModifiedUserFile_Table)item!;
        }
    }    

        
    [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> Contents_EqualValue( byte[]  value)
    {
        var sc = GeneratorFromDB.SearchCriteria.Equal;
        await foreach (var item in _search.ModifiedUserFileSimpleSearch_Contents(sc, value))
        {
            yield return (ModifiedUserFile_Table)item!;
        }
    }
    [HttpGet]
    public async IAsyncEnumerable<ModifiedUserFile_Table> Contents_DifferentValue( byte[]  value)
    {
        var sc = GeneratorFromDB.SearchCriteria.Different;
        await foreach (var item in _search.ModifiedUserFileSimpleSearch_Contents(sc, value))
        {
            yield return (ModifiedUserFile_Table)item!;
        }
    }
    [HttpGet]
    public  async IAsyncEnumerable<ModifiedUserFile_Table> Contents_SimpleSearch(GeneratorFromDB.SearchCriteria sc,  byte[] value){
        await foreach(var item in _search.ModifiedUserFileSimpleSearch_Contents(sc,value))
        {
            yield return (ModifiedUserFile_Table)item!;
        }
    }

             


    


}//end class
