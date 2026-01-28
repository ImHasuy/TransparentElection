
function footer (){
    return(
        <div className="text-center pt-1 pb-2 pr-3.5 pl-3.5 mt-10 bg-white text-black items-center border-t border-t-gray-400">
            <div className="container mx-auto px-4 sm:px-6 lg:px-8" >
                <div className="flex justify-center items-center">
                    <img src="./removedbg.png" className="justify-self-center h-30"/>
                    <p className="ml-5">
                        Tel.:
                        +36-1-234-5678
                    </p>
                    <p className="ml-5">
                        E-mail:
                        info@blockchainvote.hu, sajto@blockchainvote.hu, international@blockchainvote.hu
                    </p>
                </div>
            </div>
        </div>
    );
}

export default footer;