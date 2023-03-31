﻿//this was autogenerated by a tool. Do not modify! Use partial
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Generated;

public partial class SearchModifiedUser:  GeneratorFromDB.Search<eModifiedUserColumns,ModifiedUser>
{
    //private ApplicationDBContext _context;
    //public SearchModifiedUser(ApplicationDBContext context){
    //    _context= context;
    //}
public static SearchModifiedUser FromSearch(GeneratorFromDB.SearchCriteria sc, eModifiedUserColumns colToSearch, string value)
    {
        var search = new SearchModifiedUser();
        var orderBy = new GeneratorFromDB.OrderBy<eModifiedUserColumns>();
                orderBy.FieldName = eModifiedUserColumns.IDUser ;;
        
        orderBy.Asc = false;
        search.OrderBys = new[] { orderBy };
        search.PageNumber = 1;
        search.PageSize = int.MaxValue - 1;
        var s = new GeneratorFromDB.SearchField<eModifiedUserColumns>();
        s.Criteria = sc;
        s.FieldName = colToSearch;
        s.Value = value;
        search.SearchFields = new[] { s };
        return search;
    }
   public override IOrderedQueryable<ModifiedUser> TransformToOrder(IQueryable<ModifiedUser> data){
        if(OrderBys == null || OrderBys.Length ==0){
            OrderBys =new GeneratorFromDB.OrderBy<eModifiedUserColumns>[1];
            OrderBys[0]= new GeneratorFromDB.OrderBy<eModifiedUserColumns>()
            {
                //maybe find PK ...
                FieldName = eModifiedUserColumns.IDUser,
                Asc=false
            };
        }
        var order = OrderBys[0]!;
        IOrderedQueryable<ModifiedUser> ret;
        //TODO: maybe utilize EF.Property ? 
        switch(order.FieldName){
                    case eModifiedUserColumns.IDUser:
                if(order.Asc)
                    ret = data.OrderBy(it=>it.IDUser);
                else
                    ret = data.OrderByDescending(it=>it.IDUser);
                
                break;

                    case eModifiedUserColumns.NameUser:
                if(order.Asc)
                    ret = data.OrderBy(it=>it.NameUser);
                else
                    ret = data.OrderByDescending(it=>it.NameUser);
                
                break;

                    default:
                throw new ArgumentException(" cannot order ModifiedUser by "+ order.FieldName);
            
        }
        for(var i=1;i<OrderBys.Length;i++){
            order=OrderBys[i];
            switch(order.FieldName){
                    case eModifiedUserColumns.IDUser:
                if(order.Asc)
                    ret = ret.ThenBy(it=>it.IDUser);
                else
                    ret = ret.ThenByDescending(it=>it.IDUser);
                
                break;
                    case eModifiedUserColumns.NameUser:
                if(order.Asc)
                    ret = ret.ThenBy(it=>it.NameUser);
                else
                    ret = ret.ThenByDescending(it=>it.NameUser);
                
                break;
                    default:
                throw new ArgumentException(" cannot order ModifiedUser by "+ order.FieldName);
            
        }
        }
        return ret;
        
    }
    public override  IQueryable<ModifiedUser> TransformToWhere(IQueryable<ModifiedUser> data){
        if(SearchFields == null || SearchFields.Length ==0)        
            return data;
        var returnValue = data;
        foreach(var s in SearchFields){
            switch(s.FieldName ){
                case eModifiedUserColumns.None :
                    continue;
                    
            case eModifiedUserColumns.IDUser:
                //long isNullable False
                if(s.Value == null)
        {
                            throw new ArgumentException("ModifiedUser.IDUser cannot be null");
                                }//end if s.value is null -search for null
        //if we are here, s.Value is not null
        { //use this to define value in smaller scope
                        var valueArray = s.Value
                    .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(it=>long.Parse(it))
                    .ToArray();
                var value = valueArray[0];
                  
          
        switch(s.Criteria){

            case GeneratorFromDB.SearchCriteria.Between:
                if(valueArray?.Length != 2){
                    throw new ArgumentException("between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>it.IDUser >= valueArray[0] && it.IDUser <= valueArray[1]);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotBetween:
            
            if(valueArray?.Length != 2){
                    throw new ArgumentException("not between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>it.IDUser < valueArray[0] || it.IDUser > valueArray[1]);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.InArray:
                        returnValue =returnValue.Where(it=> valueArray!.Contains(it.IDUser));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotInArray:
                        returnValue =returnValue.Where(it=> !valueArray!.Contains(it.IDUser));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.Equal:
                returnValue =returnValue.Where(it=>it.IDUser==value);
                continue;
            case GeneratorFromDB.SearchCriteria.Different:
                returnValue =returnValue.Where(it=>it.IDUser!=value);
                continue;
            
            case GeneratorFromDB.SearchCriteria.Less:
                                                returnValue =returnValue.Where(it=>it.IDUser<value);
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.LessOrEqual:
                                                
                        returnValue =returnValue.Where(it=>it.IDUser<=value);
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.Greater:
                                                
                        returnValue =returnValue.Where(it=>it.IDUser>value);
                                                continue;
                    case GeneratorFromDB.SearchCriteria.GreaterOrEqual:
                                                returnValue =returnValue.Where(it=>it.IDUser>=value);
                                                continue;
                                                                        

            default:
                throw new ArgumentException($"not found Criteria {(int)s.Criteria} {s.Criteria} for {s.FieldName}");
        }//end switch after s.Criteria

                //continue;
        } //end use this to define value in smaller scope
            
                    
            case eModifiedUserColumns.NameUser:
                //long isNullable False
                if(s.Value == null)
        {
                            throw new ArgumentException("ModifiedUser.NameUser cannot be null");
                                }//end if s.value is null -search for null
        //if we are here, s.Value is not null
        { //use this to define value in smaller scope
                        var valueArray = s.Value
                    .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(it=>long.Parse(it))
                    .ToArray();
                var value = valueArray[0];
                  
          
        switch(s.Criteria){

            case GeneratorFromDB.SearchCriteria.Between:
                if(valueArray?.Length != 2){
                    throw new ArgumentException("between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>it.NameUser >= valueArray[0] && it.NameUser <= valueArray[1]);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotBetween:
            
            if(valueArray?.Length != 2){
                    throw new ArgumentException("not between must have 2 args, separated by comma => value is:" + s.Value);
                }
                                    returnValue =returnValue.Where(it=>it.NameUser < valueArray[0] || it.NameUser > valueArray[1]);
                  
                continue;
            case GeneratorFromDB.SearchCriteria.InArray:
                        returnValue =returnValue.Where(it=> valueArray!.Contains(it.NameUser));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.NotInArray:
                        returnValue =returnValue.Where(it=> !valueArray!.Contains(it.NameUser));
                  
                continue;
            case GeneratorFromDB.SearchCriteria.Equal:
                returnValue =returnValue.Where(it=>it.NameUser==value);
                continue;
            case GeneratorFromDB.SearchCriteria.Different:
                returnValue =returnValue.Where(it=>it.NameUser!=value);
                continue;
            
            case GeneratorFromDB.SearchCriteria.Less:
                                                returnValue =returnValue.Where(it=>it.NameUser<value);
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.LessOrEqual:
                                                
                        returnValue =returnValue.Where(it=>it.NameUser<=value);
                                                
                        continue;
                    case GeneratorFromDB.SearchCriteria.Greater:
                                                
                        returnValue =returnValue.Where(it=>it.NameUser>value);
                                                continue;
                    case GeneratorFromDB.SearchCriteria.GreaterOrEqual:
                                                returnValue =returnValue.Where(it=>it.NameUser>=value);
                                                continue;
                                                                        

            default:
                throw new ArgumentException($"not found Criteria {(int)s.Criteria} {s.Criteria} for {s.FieldName}");
        }//end switch after s.Criteria

                //continue;
        } //end use this to define value in smaller scope
            
                }//end switch  
       }//end foreach
    return returnValue;
    //throw new NotImplementedException("not");
    }

    public long IDUser { get; set; }

    public long NameUser { get; set; }

    //[InverseProperty("IDUserNavigation")]
//    public virtual ICollection<ModifiedUserFile> ModifiedUserFile { get; } = new List<ModifiedUserFile>();
}
