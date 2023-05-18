import { Outlet } from 'react-router-dom';
import { Header } from './components/common/Header';
import { SocialMediaIcons } from './components/common/SocialMediaIcons';
import { useState } from 'react';

export function loader() {
  console.log('soy el loader');
}

function App() {
  return (
    <div className='mx-auto flex max-h-fit min-h-screen flex-col justify-between bg-fondo-celu bg-cover bg-no-repeat sm:bg-fondo-pc select-none'>
      <Layout />
      <SocialMediaIcons />
    </div>
  );
}

export const Layout = () => {
  const [isHidden, setIsHidden] = useState(false);
  const showLayout = () => setIsHidden(false);
  const hideLayout = () => setIsHidden(true);

  return (
    <div className='mx-auto sm:max-w-4xl lg:max-w-4xl xl:max-w-[1500px] '>
      <Header showLayout={showLayout} hideLayout={hideLayout} />
      <main
        className={isHidden ? 'mx-auto mb-6 hidden select-none' : 'mx-auto  my-2 block select-none '}
      >
        <Outlet />
      </main>
    </div>
  );
};
export default App;
