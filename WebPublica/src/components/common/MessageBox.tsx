import { ReactNode } from 'react';

interface IProps {
  children: ReactNode;
  large?: boolean;
  type: 'error' | 'success' | 'info';
}

const backgroundByType = {
  error: 'bg-red-600',
  success: 'bg-green-600',
  info: 'bg-blue-600',
};

const MessageBox = ({ children, large, type }: IProps) => {
  let sizeClasses = 'px-2 py-1 mt-2';

  if (large) sizeClasses = 'mt-8 font-sans font-bold px-6 py-20 mx-10';

  return (
    <>
      {children && (
        <div className='font-sans'>
          <div
            className={`rounded-xl border text-center text-white ${backgroundByType[type]} ${sizeClasses}`}
          >
            {children}
          </div>
        </div>
      )}
    </>
  );
};

export default MessageBox;
