import React, { useState, useEffect } from "react";
import Spinner from 'Components/Spinner/Spinner.js';


function useFetch(url) {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);

  async function fetchUrl() {

    function handleErrors(response) {
      if (!response.ok) {
          setLoading(false);
          throw Error(response.statusText);
      }
      return response;
    }

    const response = await fetch(url).then(handleErrors)
    const json = await response.json();

    setData(json);
    setLoading(false);
  }

  useEffect(() => {
    fetchUrl()        
  }, []);
  
  return [data, loading];
}

function fetchDataAndRenderResponse(url, functionThatRenderResponse) {
  const [data, loading] = useFetch(url);    
  
  if (loading) {
    return <Spinner />;
  }          
  else {
    return functionThatRenderResponse(data);
  }
}



export { useFetch, fetchDataAndRenderResponse };