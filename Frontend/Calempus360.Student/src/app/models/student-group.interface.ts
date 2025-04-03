import { Option } from "./option.interface"
import { Site } from "./site.interface"

export type StudentGroup = {
    id?: string,
    code?: string,
    numberOfStudents?: number,
    optionGrade?: number,
    site?: Site,
    option?: Option
}

