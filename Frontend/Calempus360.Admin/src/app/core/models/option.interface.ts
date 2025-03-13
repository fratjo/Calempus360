import { Course } from "./course.interface"

export type Option = {
    id?: string,
    name?: string,
    code?: string,
    description?: string,
    courses?: Course[]
}