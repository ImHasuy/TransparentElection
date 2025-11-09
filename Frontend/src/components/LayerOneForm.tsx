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


const formSchema = z.object({
    IDCardNumber: z.string().min(3),
    ResidenceCardNumber: z.string().min(3)
})

export function FirstLayerForm() {
    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            IDCardNumber: "",
            ResidenceCardNumber: ""
        }
    })

    return (

        <div className="content-center m-10">
            <Form {...form}>
                <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
                    <FormField
                        control={form.control}
                        name="IDCardNumber"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel className="text-black font-bold">Kerem irja be a Személyigazolványa számat!</FormLabel>
                                <FormControl>
                                    <Input placeholder="Pl.: 123456789" {...field} />
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
                                <FormLabel className="text-black font-bold">Kerem irja be a Lakcím kártyaja számat</FormLabel>
                                <FormControl>
                                    <Input placeholder="Pl.: 987654321" {...field} />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                    <Button type="submit" variant="gradient" className="font-bold">Adatok elkuldese</Button>
                </form>
            </Form>
        </div>
    )
}

function onSubmit(data: z.infer<typeof formSchema>) {
    console.log(data)
}


