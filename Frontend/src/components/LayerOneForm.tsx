"use client"

import {z} from 'zod'
import {zodResolver} from '@hookform/resolvers/zod'
import {useForm} from 'react-hook-form'
import {
    Form,
    FormControl,
    FormDescription,
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
            IDCardNumber: "Pl.: 12456789",
            ResidenceCardNumber: "Pl.: 987654321"
        }
    })
    return (
        <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
                <FormField
                    control={form.control}
                    name="IDCardNumber"
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel>Személyigazolványszám</FormLabel>
                            <FormControl>
                                <Input placeholder="Pl.: 123456789" {...field} />
                            </FormControl>
                            <FormDescription>
                                A személyi igazolványának a száma
                            </FormDescription>
                            <FormMessage />
                        </FormItem>
                    )}
                />
                <FormField
                    control={form.control}
                    name="ResidenceCardNumber"
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel>Lakcím kárty száma</FormLabel>
                            <FormControl>
                                <Input placeholder="Pl.: 987654321" {...field} />
                            </FormControl>
                            <FormDescription>
                                A lakcímkártyájának a száma.
                            </FormDescription>
                            <FormMessage />
                        </FormItem>
                    )}
                />
                <Button type="submit">Submit</Button>
            </form>
        </Form>
    )
}

function onSubmit(data: z.infer<typeof formSchema>) {
    console.log(data)
}


