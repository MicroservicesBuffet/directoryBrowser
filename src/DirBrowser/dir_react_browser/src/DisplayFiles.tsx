import {
    QueryClient,
    QueryClientProvider,
    useQuery,
  } from '@tanstack/react-query'
import { useState } from 'react'
import { FolderToRead } from './genericFiles/FolderToRead';
  

  export default function DisplayFiles(){

    const [nrFiles, setNrFiles]=useState<number|null>(null);
    
    const { isLoading, error, data } = useQuery({
        //queryKey: ['repoData'],
        queryFn: () =>
          fetch('http://localhost:5288/api/v1.0/File/GetRootFolders').then(
            (res) => res.json() ,
          ),
      })
    
      if (isLoading) return <>'Loading...'</>
    
      if (error) return <>'An error has occurred: ' + JSON.stringify(error)</>
    

    return <>Files : {JSON.stringify(data)} </>
  }