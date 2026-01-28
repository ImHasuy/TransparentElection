"use client"
import {z} from 'zod'
import {zodResolver} from '@hookform/resolvers/zod'
import {useForm} from 'react-hook-form'
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage
} from "@/components/ui/form.tsx";
import {Button} from "@/components/ui/button.tsx";
import {Input} from "@/components/ui/input.tsx";
import type FirstLayerPostInputDto from "@/interfaces/FirstLayerPostInputDto.ts";
import UseAuthForVoters from "@/hooks/useAuthForVoters.tsx";
import {useNavigate} from "react-router-dom";
import {useState} from "react";


const formSchema = z.object({
    IDCardNumber: z.string().min(3, {message: "Legalább 3 karater hosszú kell legyen a bemenet!"}),
    ResidenceCardNumber: z.string().min(3, {message: "Legalább 3 karater hosszú kell legyen a bemenet!"})
})


export function FirstLayerForm() {
    const { login } = UseAuthForVoters()
    const navigate = useNavigate();
    const [disabled, setDisabled] = useState<boolean>(false)

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            IDCardNumber: "",
            ResidenceCardNumber: ""
        }
    })

    async function onSubmit(data: z.infer<typeof formSchema>) {
        form.clearErrors()
        const apiData :FirstLayerPostInputDto ={
            idcardnumber: data.IDCardNumber,
            residencecardnumber: data.ResidenceCardNumber
        }

        try{
            setDisabled(true);
            await login(apiData.idcardnumber, apiData.residencecardnumber);
            navigate("/LayerTwoVerification")
            setDisabled(false);
        }catch(e){
            setDisabled(false);
            console.log(e)
            form.setError("ResidenceCardNumber", {
                type: "server",
                message: "A megadott adatok hibásak, próbálja újra!"
            })
            form.setError("IDCardNumber", {
                type: "server",
                message: "A megadott adatok hibásak, próbálja újra!"
            })
        }


        console.log(data)
    }

    return (
        <div className="content-center m-10 text-1xl">
            <Form  {...form}>
                <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
                    <FormField
                        control={form.control}
                        name="IDCardNumber"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel className="text-black font-bold text-2xl">Kérem írja be a személyigazolványa számát!</FormLabel>
                                <FormControl>
                                    <Input className="text-black" placeholder="Pl.: 123456789" {...field} />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                    <FormField
                        control={form.control}
                        name="ResidenceCardNumber"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel  className="text-black font-bold text-2xl text-center">Kérem írja be a lakcímkártyája számát!</FormLabel>
                                <FormControl>
                                    <Input className="text-black" placeholder="Pl.: 987654321" {...field} />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                    <Button type="submit" variant="gradient" className="font-bold w-70" style={{fontFamily: "Momo Trust Display"}}  size={"summaryButton"} disabled={disabled}>Adatok elküldése</Button>
                </form>
            </Form>
        </div>
    )
}




