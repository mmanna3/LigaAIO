import { useEffect, useState } from 'react';
import { BASE_URL } from '../../../globalConst';

/* TRUCAZO! Typescript */
export const useFetch = <TipoDeDato>(endpoint: string) => {
  const [data, setData] = useState<TipoDeDato[]>([]);
  const [isFetching, setIsFetching] = useState(true);

  async function doFetch() {
    const response = await fetch(`${BASE_URL}/publico/${endpoint}`);

    const json = await response.json();

    // console.log(json);

    setData(json);
    setIsFetching(false);
  }

  useEffect(() => {
    doFetch();
  }, []);

  return { data, isFetching };
};
