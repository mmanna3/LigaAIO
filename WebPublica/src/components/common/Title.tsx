interface Props {
  img: string
  alt: string
}

export const Title = ({img, alt}: Props) => {
  return <img className='mx-auto mb-5 lg:max-w-4xl ' src={img} alt={alt} />
}